using MD3.CatalogoSaaS.API.Helpers;
using MD3.CatalogoSaaS.Data.EF;
using MD3.CatalogoSaaS.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    Console.WriteLine("#################################### MODO DEV ####################################\r\n");
}

CatalogoSaaSContext.connectionStringDefault = builder.Configuration.GetConnectionString("Default");

#region Middleware de autenticação

app.Use(async (context, next) =>
{
    string? authHeader = context.Request.Headers["md3-csaas-key"];
    List<string> ChavesAutorizadas = new()
    {
        "BC2D8987-0880-495A-B288-78194DE2EE94",
        "B86C1ED1-D5CD-46B2-82A1-D83B18DF0B68"
    };

    if (authHeader == null || !ChavesAutorizadas.Contains(authHeader))
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsJsonAsync("Acesso negado!");
        return;
    }

    await next(context);
});

#endregion

app.UseHttpsRedirection();



app.MapGet("/GetSystemConfigs/{systemCode}", (string systemCode) =>
{
    using var _db = new CatalogoSaaSContext();

    if (!_db.Sistemas.Any(r => r.CodigoInterno == systemCode))
        return null;

    var configuracoes = _db.ConfiguracoesDeSistema
        .AsNoTracking()
        .Where(r => r.Sistema.CodigoInterno == systemCode)
        .Select(r => new
        {
            Chave = r.Parametro.CodigoUnico,
            Valor = r.Valor
        })
        .ToArray();

#if DEBUG
    Console.WriteLine($"GetSystemConfigs: {JsonSerializer.Serialize(configuracoes)}");
#endif

    return configuracoes;
});


app.MapGet("/GetTennants/{user}", (string user) =>
{
    using var _db = new CatalogoSaaSContext();

    if (!_db.Usuarios.Any(r => r.Email == user))
        return null;

    var tenants = _db.ContasDeSistema
        .AsNoTracking()
        .Where(r => r.Usuarios.Any(i => i.Email == user))
        .Select(r => new
        {
            Conta = r.Id.Value,
            Plano = r.Plano_Id.Value,
            Sistema = r.Sistema.CodigoInterno,
            Tenant = r.Tenant_Id.Value,
            TenantNome = r.Tenant.Nome
        })
        .ToArray();

#if DEBUG
    Console.WriteLine($"GetTennants: {JsonSerializer.Serialize(tenants)}");
#endif

    return tenants;
});

app.MapGet("/GetAuthTypeConfig/{account}", async (string account) =>
{
    if (!int.TryParse(account, out int _accountId))
        return null;

    using var _db = new CatalogoSaaSContext();

    var authType = await _db.ConfiguracoesDeContasDeSistema
        .AsNoTracking()
        .Where(r =>
            r.Conta_Id == _accountId &&
            r.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AUTH_TYPE)
        .Select(r => r.Valor)
        .FirstOrDefaultAsync();

#if DEBUG
    Console.WriteLine($"GetAuthTypeConfig: {authType}");
#endif

    return new { authType };
});

app.MapGet("/AzureAdConfigured/{account}", async (string account) =>
{
    if (!int.TryParse(account, out int _accountId))
        return false;

    using var _db = new CatalogoSaaSContext();

    var value = await _db.ConfiguracoesDeContasDeSistema
        .AsNoTracking()
        .AnyAsync(r => r.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AZURE_AD_TENANT_ID && r.Conta_Id == _accountId && !string.IsNullOrWhiteSpace(r.Valor));

#if DEBUG
    Console.WriteLine($"AzureAdConfigured: {JsonSerializer.Serialize(value)}");
#endif

    return value;
});


app.MapGet("/GetAccountConfigs/{account}", async (string account) =>
{
    if (!int.TryParse(account, out int _accountId))
        return null;

    using var _db = new CatalogoSaaSContext();

    var configuracoes = await _db.ConfiguracoesDeContasDeSistema
        .AsNoTracking()
        .Where(r => r.Conta_Id == _accountId)
        .Select(r => new
        {
            Chave = r.Parametro.CodigoUnico,
            Valor = r.Valor
        })
        .ToArrayAsync();

#if DEBUG
    Console.WriteLine($"GetAuthTypeConfig: {JsonSerializer.Serialize(configuracoes)}");
#endif

    return configuracoes;
});

app.MapGet("/GetAccountConfigsByDomain/{domain}", async (string domain) =>
{
    if (string.IsNullOrWhiteSpace(domain))
        return null;

    using var _db = new CatalogoSaaSContext();

    var contaId = await _db.ContasDeSistema
        .AsNoTracking()
        .Where(r => r.ConfiguracoesDaConta.Any(i => i.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.FRONT_DOMAIN && i.Valor.Contains(domain)))
        .Select(r => r.Id)
        .FirstOrDefaultAsync();

    if (contaId == null)
        return null;

    var configuracoes = await _db.ConfiguracoesDeContasDeSistema
        .AsNoTracking()
        .Where(r => r.Conta_Id == contaId)
        .Select(r => new
        {
            Chave = r.Parametro.CodigoUnico,
            Valor = r.Valor
        })
        .ToArrayAsync();

#if DEBUG
    Console.WriteLine($"GetAccountConfigsByDomain: {JsonSerializer.Serialize(configuracoes)}");
#endif

    return configuracoes;
});


app.MapGet("/GetAllTennantsConnectionString/{systemCode}", (string systemCode) =>
{
    using var _db = new CatalogoSaaSContext();

    if (!_db.Sistemas.Any(r => r.CodigoInterno == systemCode))
        return null;

    var tenants = _db.ContasDeSistema
        .AsNoTracking()
        .Where(r => r.Sistema.CodigoInterno == systemCode)
        .Select(r => new
        {
            Conta = r.Id,
            r.ConfiguracoesDaConta.FirstOrDefault(r => r.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.DB_CONNECTION_STRING).Valor
        })
        .ToArray();

#if DEBUG
    Console.WriteLine($"GetAllTennantsConnectionString: {JsonSerializer.Serialize(tenants)}");
#endif

    return tenants;
});

app.MapGet("/GetAllConnectionString", async () =>
{
    using var _db = new CatalogoSaaSContext();

    var values = await _db.ConfiguracoesDeContasDeSistema
        .Where(r => r.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.DB_CONNECTION_STRING && !string.IsNullOrWhiteSpace(r.Valor))
        .AsNoTracking()
        .Select(r => new
        {
            Conta = r.Conta_Id,
            r.Valor
        })
        .ToArrayAsync();

#if DEBUG
    Console.WriteLine($"GetAllConnectionString: {JsonSerializer.Serialize(values)}");
#endif

    return values;
});

app.MapGet("/GetAllAzureAdTenants", async () =>
{
    using var _db = new CatalogoSaaSContext();

    var values = await _db.ConfiguracoesDeContasDeSistema
        .Where(r => r.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AZURE_AD_TENANT_ID && !string.IsNullOrWhiteSpace(r.Valor))
        .AsNoTracking()
        .Select(r => r.Valor)
        .Distinct()
        .ToArrayAsync();

#if DEBUG
    Console.WriteLine($"GetAllAzureAdTenants: {JsonSerializer.Serialize(values)}");
#endif

    return values;
});


app.MapPost("/NewUserAccount/{account}/{user}", async (string account, string user) =>
{
    if (!int.TryParse(account, out int _accountId))
    {
#if DEBUG
        Console.WriteLine("Código de conta inválido");
#endif
        return Results.Problem("Código de conta inválido");
    }

    using var _db = new CatalogoSaaSContext();

    var _user = await _db.Usuarios
        .FirstOrDefaultAsync(r => r.Email == user)
        ?? new Usuario(user, user);

    var _account = await _db.ContasDeSistema
        .Include(r => r.Usuarios.Where(i => i.Id == _user.Id))
        .FirstOrDefaultAsync(r =>
            r.Id == _accountId &&
            r.Usuarios.Any()
        );

    if (_account == null)
    {
#if DEBUG
        Console.WriteLine("Conta não encontrada");
#endif
        return Results.Problem("Conta não encontrada");
    }

    if (!_account.Usuarios.Any(r => r == _user))
        _account.AdicionarUsuario(_user);

    await _db.SaveChangesAsync();

#if DEBUG
    Console.WriteLine("NewUserAccount: OK");
#endif

    return Results.Ok();
});

app.MapPost("/DeleteUserAccount/{account}/{user}", async (string account, string user) =>
{
    if (!int.TryParse(account, out int _accountId))
    {
#if DEBUG
        Console.WriteLine("Código de conta/tenant inválido");
#endif
        return Results.Problem("Código de conta/tenant inválido");
    }

    using var _db = new CatalogoSaaSContext();

    var _user = await _db.Usuarios.FirstOrDefaultAsync(r => r.Email == user);
    if (_user == null)
    {
#if DEBUG
        Console.WriteLine("Usuário não localizado");
#endif
        return Results.Problem("Usuário não localizado");
    }

    if (!await _db.ContasDeSistema.AnyAsync(r => r.Id == _accountId))
    {
#if DEBUG
        Console.WriteLine("Conta não encontrada");
#endif
        return Results.Problem("Conta não encontrada");
    }

    var _account = await _db.ContasDeSistema
        .Include(r => r.Usuarios)
        .FirstOrDefaultAsync(r => r.Id == _accountId);

    if (!_account.ExisteUsuario(_user))
        return Results.Ok();

    _account.RemoverUsuario(_user);
    await _db.SaveChangesAsync();

#if DEBUG
    Console.WriteLine("DeleteUserAccount: OK");
#endif

    return Results.Ok();
});



app.Run();
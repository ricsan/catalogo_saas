using MD3.CatalogoSaaS.Data.EF;
using MD3.CatalogoSaaS.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.API.Helpers
{
    public static class MD3ContaDeSistemaFactory
    {
        public static async Task<ContaDeSistema> NovaContaPadraoComAzureAD(
            Sistema sistema,
            string nomeDaOrganizacao,
            (string, string)[] usuarios,
            string stringDeConexao,
            string awsBucketName,
            string awsBucketAccessKey,
            string awsBucketSecretKey,
            string awsBucketDomain,
            string dominiosDoFront)
        {
            using var _db = new CatalogoSaaSContext();

            var tenant = await _db.Tenants
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Nome == nomeDaOrganizacao)
                ?? new Tenant(nomeDaOrganizacao);

            var novaConta = ContaDeSistema.New(
                tenant,
                sistema,
                sistema.Parametros?.ToArray(),
                sistema.Planos?.LastOrDefault()
            );

            foreach (var configuracao in novaConta.ConfiguracoesDaConta)
            {
                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.FRONT_DOMAIN)
                    configuracao.DefinirValor(dominiosDoFront);


                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AUTH_TYPE)
                    configuracao.DefinirValor($"{TiposComunsDeAutenticacao.EMAIL_SENHA},{TiposComunsDeAutenticacao.AZURE_AD}");


                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.DB_CONNECTION_STRING)
                    configuracao.DefinirValor(stringDeConexao);


                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_NAME)
                    configuracao.DefinirValor(awsBucketName);

                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_ACCESS_KEY)
                    configuracao.DefinirValor(awsBucketAccessKey);

                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_SECRET_KEY)
                    configuracao.DefinirValor(awsBucketSecretKey);

                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_DOMAIN && !string.IsNullOrWhiteSpace(awsBucketDomain))
                    configuracao.DefinirValor(awsBucketDomain);

                if (configuracao.Parametro.CodigoUnico == CodigosComunsDeParametrosDeConta.AWS_S3_BUCKET_REGION)
                    configuracao.DefinirValor("USEast1");
            }

            foreach (var usuario in usuarios)
            {
                var novoUsuario =
                    await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(r => r.Email == usuario.Item2)
                    ?? new Usuario(usuario.Item1, usuario.Item2);

                novaConta.AdicionarUsuario(novoUsuario);
            }

            novaConta.LimparEntidadesDeRelacionamento();

            return novaConta;
        }


        public static async Task<ContaDeSistema> NovaContaPadraoNoCMSComAzureAD(
            string nomeDaOrganizacao,
            string stringDeConexao,
            string awsBucketName,
            string awsBucketAccessKey,
            string awsBucketSecretKey,
            string awsBucketDomain,
            string dominiosDoFront)
        {
            using var _db = new CatalogoSaaSContext();

            var sistema = await _db.Sistemas
                .AsNoTracking()
                .Include(r => r.Planos)
                .Include(r => r.Parametros)
                .FirstOrDefaultAsync(r => r.CodigoInterno == "AA00");

            (string, string)[] usuarios = BuscarUsuariosNosSistemasDaMD3(stringDeConexao);

            var contaCMS = await MD3ContaDeSistemaFactory.NovaContaPadraoComAzureAD(
                sistema,
                nomeDaOrganizacao,
                usuarios,
                stringDeConexao,
                awsBucketName,
                awsBucketAccessKey,
                awsBucketSecretKey,
                awsBucketDomain,
                dominiosDoFront
            );

            return contaCMS;
        }

        public static async Task<ContaDeSistema> NovaContaPadraoNoRepositorioComAzureAD(
            string nomeDaOrganizacao,
            string stringDeConexao,
            string awsBucketName,
            string awsBucketAccessKey,
            string awsBucketSecretKey,
            string awsBucketDomain,
            string dominiosDoFront)
        {
            using var _db = new CatalogoSaaSContext();

            var sistema = await _db.Sistemas
                .AsNoTracking()
                .Include(r => r.Planos)
                .Include(r => r.Parametros)
                .FirstOrDefaultAsync(r => r.CodigoInterno == "AA01");

            (string, string)[] usuarios = BuscarUsuariosNosSistemasDaMD3(stringDeConexao);

            var contaCMS = await MD3ContaDeSistemaFactory.NovaContaPadraoComAzureAD(
                sistema,
                nomeDaOrganizacao,
                usuarios,
                stringDeConexao,
                awsBucketName,
                awsBucketAccessKey,
                awsBucketSecretKey,
                awsBucketDomain,
                dominiosDoFront
            );

            return contaCMS;
        }

        public static async Task<ContaDeSistema> NovaContaPadraoNoBlogComAzureAD(
            string nomeDaOrganizacao,
            string stringDeConexao,
            string awsBucketName,
            string awsBucketAccessKey,
            string awsBucketSecretKey,
            string awsBucketDomain,
            string dominiosDoFront)
        {
            using var _db = new CatalogoSaaSContext();

            var sistema = await _db.Sistemas
                .AsNoTracking()
                .Include(r => r.Planos)
                .Include(r => r.Parametros)
                .FirstOrDefaultAsync(r => r.CodigoInterno == "AA02");

            (string, string)[] usuarios = BuscarUsuariosNosSistemasDaMD3(stringDeConexao);

            var contaCMS = await MD3ContaDeSistemaFactory.NovaContaPadraoComAzureAD(
                sistema,
                nomeDaOrganizacao,
                usuarios,
                stringDeConexao,
                awsBucketName,
                awsBucketAccessKey,
                awsBucketSecretKey,
                awsBucketDomain,
                dominiosDoFront
            );

            return contaCMS;
        }


        public static void CriarContaNoCMSRepositorioBlog(
            string nomeDaOrganizacao,
            string stringDeConexao,
            string awsBucketName,
            string awsBucketAccessKey,
            string awsBucketSecretKey,
            string awsBucketDomain,
            string dominiosDoFront)
        {
            using (CatalogoSaaSContext dbContext = new CatalogoSaaSContext())
            {
                var contaCMS = MD3ContaDeSistemaFactory.NovaContaPadraoNoCMSComAzureAD(
                    nomeDaOrganizacao,
                    stringDeConexao,
                    awsBucketName,
                    awsBucketAccessKey,
                    awsBucketSecretKey,
                    awsBucketDomain,
                    dominiosDoFront)
                .Result;

                if (!dbContext.ContasDeSistema.Any(r => r.Tenant.Nome == nomeDaOrganizacao && r.Sistema_Id == contaCMS.Sistema_Id))
                {
                    foreach (var item in contaCMS.Usuarios)
                    {
                        if (item.Id != null && item.Id > 0)
                            dbContext.Usuarios.Attach(item);
                    }

                    dbContext.ContasDeSistema.Add(contaCMS);
                    dbContext.SaveChanges();
                }
            }

            using (CatalogoSaaSContext dbContext = new CatalogoSaaSContext())
            {
                var contaRepositorio = MD3ContaDeSistemaFactory.NovaContaPadraoNoRepositorioComAzureAD(
                    nomeDaOrganizacao,
                    stringDeConexao,
                    awsBucketName,
                    awsBucketAccessKey,
                    awsBucketSecretKey,
                    awsBucketDomain,
                    dominiosDoFront)
                .Result;

                if (!dbContext.ContasDeSistema.Any(r => r.Tenant.Nome == nomeDaOrganizacao && r.Sistema_Id == contaRepositorio.Sistema_Id))
                {
                    foreach (var item in contaRepositorio.Usuarios)
                    {
                        if (item.Id != null && item.Id > 0)
                            dbContext.Usuarios.Attach(item);
                    }

                    dbContext.ContasDeSistema.Add(contaRepositorio);
                    dbContext.SaveChanges();
                }
            }

            using (CatalogoSaaSContext dbContext = new CatalogoSaaSContext())
            {
                var contaBlog = MD3ContaDeSistemaFactory.NovaContaPadraoNoBlogComAzureAD(
                    nomeDaOrganizacao,
                    stringDeConexao,
                    awsBucketName,
                    awsBucketAccessKey,
                    awsBucketSecretKey,
                    awsBucketDomain,
                    dominiosDoFront)
                .Result;

                if (!dbContext.ContasDeSistema.Any(r => r.Tenant.Nome == nomeDaOrganizacao && r.Sistema_Id == contaBlog.Sistema_Id))
                {
                    foreach (var item in contaBlog.Usuarios)
                    {
                        if (item.Id != null && item.Id > 0)
                            dbContext.Usuarios.Attach(item);
                    }

                    dbContext.ContasDeSistema.Add(contaBlog);
                    dbContext.SaveChanges();
                }
            }
        }


        /// <summary>
        /// Busca no CMS, Repositório e Blog.
        /// </summary>
        /// <param name="stringDeConexao"></param>
        /// <returns></returns>
        private static (string, string)[] BuscarUsuariosNosSistemasDaMD3(string stringDeConexao)
        {
            List<(string, string)> retorno = new List<(string, string)>();

            using (var conn = new SqlConnection(stringDeConexao + "Trust Server Certificate=True;Trusted_Connection=True;Integrated Security=False"))
            {
                using (var comm = new SqlCommand("SELECT [Nome],[Email] FROM [UsuarioSistema]", conn))
                {
                    conn.Open();
                    var resultadoComm = comm.ExecuteReader();

                    if (resultadoComm.HasRows)
                    {
                        while (resultadoComm.Read())
                        {
                            if (resultadoComm[0] != null && resultadoComm[1] != null)
                                retorno.Add((resultadoComm[0].ToString(), resultadoComm[1].ToString()));
                        }
                    }

                    resultadoComm.Close();
                    conn.Close();
                }
            }

            return retorno.ToArray();
        }
    }
}

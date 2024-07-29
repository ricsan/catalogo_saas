using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.TenantRoot.Tenants
{
    public class IndexModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public IndexModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IList<TenantView> Tenant { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tenants != null)
            {
                Tenant = await _context.Tenants
                    .Include(r => r.Contas)
                        .ThenInclude(r => r.Sistema)
                        .ThenInclude(r => r.Planos)
                    .Select(r => new TenantView
                    {
                        Id = r.Id,
                        IdGeral = r.IdGeral,
                        Nome = r.Nome,
                        Pai_Id = r.Pai_Id,
                        Contas = r.Contas.Select(i => new Tuple<string, string>(i.Sistema.Nome, i.Plano.Nome).ToValueTuple())
                    })
                    .ToListAsync();
            }
        }
    }
}

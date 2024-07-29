using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.ContasRoot.Contas
{
    public class IndexModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public IndexModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IList<ContaDeSistema> ContaDeSistema { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ContasDeSistema != null)
            {
                ContaDeSistema = await _context.ContasDeSistema
                    .Include(c => c.Plano)
                    .Include(c => c.Sistema)
                    .Include(c => c.Tenant)
                    .ToListAsync();
            }
        }
    }
}

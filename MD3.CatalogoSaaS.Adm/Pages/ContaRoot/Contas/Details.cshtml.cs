using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.ContasRoot.Contas
{
    public class DetailsModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DetailsModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public ContaDeSistema ContaDeSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ContasDeSistema == null)
            {
                return NotFound();
            }

            var contadesistema = await _context.ContasDeSistema
                .Include(r => r.Sistema)
                .Include(r => r.Tenant)
                .Include(r => r.Plano)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contadesistema == null)
            {
                return NotFound();
            }
            else
            {
                ContaDeSistema = contadesistema;
            }
            return Page();
        }
    }
}

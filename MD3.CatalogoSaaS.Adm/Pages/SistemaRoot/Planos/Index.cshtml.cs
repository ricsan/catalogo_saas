using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Planos
{
    public class IndexModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public IndexModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IList<PlanoDoSistema> PlanoDoSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? sistemaId)
        {
            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            if (_context.PlanosDeSistema != null)
            {
                PlanoDoSistema = await _context.PlanosDeSistema
                    .Include(p => p.Sistema)
                    .Where(r => r.Sistema_Id == sistemaId)
                    .ToListAsync();
            }

            return Page();
        }
    }
}

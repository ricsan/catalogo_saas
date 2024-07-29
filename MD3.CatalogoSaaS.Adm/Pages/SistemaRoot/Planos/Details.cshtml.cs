using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Planos
{
    public class DetailsModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DetailsModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public PlanoDoSistema PlanoDoSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? sistemaId)
        {
            if (id == null || _context.PlanosDeSistema == null)
            {
                return NotFound();
            }

            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            var planodosistema = await _context.PlanosDeSistema
                .Include(r => r.Sistema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planodosistema == null)
            {
                return NotFound();
            }
            else
            {
                PlanoDoSistema = planodosistema;
            }
            return Page();
        }
    }
}

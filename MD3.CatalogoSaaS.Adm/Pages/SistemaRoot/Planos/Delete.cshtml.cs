using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Planos
{
    public class DeleteModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DeleteModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PlanosDeSistema == null)
            {
                return NotFound();
            }
            var planodosistema = await _context.PlanosDeSistema.FindAsync(id);

            if (planodosistema != null)
            {
                PlanoDoSistema = planodosistema;
                _context.PlanosDeSistema.Remove(PlanoDoSistema);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { sistemaId = PlanoDoSistema.Sistema_Id });
        }
    }
}

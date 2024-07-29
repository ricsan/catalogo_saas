using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Parametros
{
    public class DeleteModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DeleteModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParametroDoSistema ParametroDoSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? sistemaId)
        {
            if (id == null || _context.ParametrosDeSistema == null)
            {
                return NotFound();
            }

            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            var parametrodosistema = await _context.ParametrosDeSistema
                .Include(r => r.Sistema)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (parametrodosistema == null)
            {
                return NotFound();
            }
            else
            {
                ParametroDoSistema = parametrodosistema;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ParametrosDeSistema == null)
            {
                return NotFound();
            }
            var parametrodosistema = await _context.ParametrosDeSistema.FindAsync(id);

            if (parametrodosistema != null)
            {
                ParametroDoSistema = parametrodosistema;
                _context.ParametrosDeSistema.Remove(ParametroDoSistema);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { sistemaId = ParametroDoSistema.Sistema_Id });
        }
    }
}

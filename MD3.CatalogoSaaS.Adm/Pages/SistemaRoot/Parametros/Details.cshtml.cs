using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Parametros
{
    public class DetailsModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DetailsModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

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
    }
}

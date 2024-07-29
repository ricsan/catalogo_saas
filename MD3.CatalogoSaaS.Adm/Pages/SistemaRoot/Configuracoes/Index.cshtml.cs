using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Configuracoes
{
    public class IndexModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public IndexModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IList<ConfiguracaoDeSistema> ConfiguracaoDeSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? sistemaId)
        {
            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            if (_context.ConfiguracoesDeSistema != null)
            {
                ConfiguracaoDeSistema = await _context.ConfiguracoesDeSistema
                    .Include(c => c.Parametro)
                    .Include(c => c.Sistema)
                    .Where(r => r.Sistema_Id == sistemaId)
                    .ToListAsync();
            }

            return Page();
        }
    }
}

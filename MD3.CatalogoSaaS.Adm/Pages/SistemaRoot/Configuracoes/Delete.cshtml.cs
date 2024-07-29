using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Configuracoes
{
    public class DeleteModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DeleteModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ConfiguracaoDeSistema ConfiguracaoDeSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? sistemaId, int? parametroId)
        {
            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            if (parametroId is null || parametroId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(parametroId)] = parametroId;

            var configuracaodesistema = await _context.ConfiguracoesDeSistema
                .Include(r => r.Sistema)
                .Include(r => r.Parametro)
                .FirstOrDefaultAsync(m => m.Sistema_Id == sistemaId && m.Parametro_Id == parametroId);

            if (configuracaodesistema == null)
            {
                return NotFound();
            }
            else
            {
                ConfiguracaoDeSistema = configuracaodesistema;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? sistemaId, int? parametroId)
        {
            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            if (parametroId is null || parametroId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(parametroId)] = parametroId;

            var configuracaodesistema = await _context.ConfiguracoesDeSistema
                .FirstOrDefaultAsync(m => m.Sistema_Id == sistemaId && m.Parametro_Id == parametroId);

            if (configuracaodesistema != null)
            {
                ConfiguracaoDeSistema = configuracaodesistema;
                _context.ConfiguracoesDeSistema.Remove(ConfiguracaoDeSistema);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { sistemaId = ConfiguracaoDeSistema.Sistema_Id });
        }
    }
}

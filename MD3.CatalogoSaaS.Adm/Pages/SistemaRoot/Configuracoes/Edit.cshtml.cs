using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Configuracoes
{
    public class EditModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public EditModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
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
                .FirstOrDefaultAsync(m => m.Sistema_Id == sistemaId && m.Parametro_Id == parametroId);
            if (configuracaodesistema == null)
            {
                return NotFound();
            }

            ConfiguracaoDeSistema = configuracaodesistema;
            ViewData["Parametro_Id"] = new SelectList(_context.ParametrosDeSistema, "Id", "CodigoUnico");
            //ViewData["Sistema_Id"] = new SelectList(_context.Sistemas, "Id", "CodigoInterno");
            ViewData["Sistema"] = _context.Sistemas.Where(r => r.Id == sistemaId).Select(r => r.Nome).FirstOrDefault();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ConfiguracaoDeSistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfiguracaoDeSistemaExists(ConfiguracaoDeSistema.Sistema_Id, ConfiguracaoDeSistema.Parametro_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { sistemaId = ConfiguracaoDeSistema.Sistema_Id });
        }

        private bool ConfiguracaoDeSistemaExists(int? sistemaId, int? parametroId)
        {
            return (_context.ConfiguracoesDeSistema?.Any(e => e.Sistema_Id == sistemaId && e.Parametro_Id == parametroId)).GetValueOrDefault();
        }
    }
}

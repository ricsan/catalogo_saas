using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.ContaRoot.Configuracoes
{
    public class EditModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public EditModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ConfiguracaoDeConta ConfiguracaoDeConta { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? contaId, int? parametroId)
        {
            if (contaId is null || contaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(contaId)] = contaId;

            if (parametroId is null || parametroId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(parametroId)] = parametroId;

            var configuracaodeconta = await _context.ConfiguracoesDeContasDeSistema
                .FirstOrDefaultAsync(m => m.Conta_Id == contaId && m.Parametro_Id == parametroId);
            if (configuracaodeconta == null)
            {
                return NotFound();
            }

            ConfiguracaoDeConta = configuracaodeconta;

            ViewData["Parametro_Id"] = new SelectList(_context.ParametrosDeSistema, "Id", "CodigoUnico");
            //ViewData["Conta_Id"] = new SelectList(_context.ContasDeSistema, "Id", "Id");
            ViewData["Conta"] = _context.ContasDeSistema
                .Include(r => r.Tenant)
                .Where(r => r.Id == contaId)
                .Select(r => r.Id + " (" + r.Tenant.Nome + ")")
                .FirstOrDefault();

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

            _context.Attach(ConfiguracaoDeConta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfiguracaoDeContaExists(ConfiguracaoDeConta.Conta_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { contaId = ConfiguracaoDeConta.Conta_Id });
        }

        private bool ConfiguracaoDeContaExists(int? id)
        {
            return (_context.ConfiguracoesDeContasDeSistema?.Any(e => e.Conta_Id == id)).GetValueOrDefault();
        }
    }
}

using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.ContaRoot.Configuracoes
{
    public class DeleteModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DeleteModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
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
                .Include(r => r.Parametro)
                .Include(r => r.Conta)
                    .ThenInclude(r => r.Tenant)
                .FirstOrDefaultAsync(m => m.Conta_Id == contaId && m.Parametro_Id == parametroId);

            if (configuracaodeconta == null)
            {
                return NotFound();
            }
            else
            {
                ConfiguracaoDeConta = configuracaodeconta;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? contaId, int? parametroId)
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

            if (configuracaodeconta != null)
            {
                ConfiguracaoDeConta = configuracaodeconta;
                _context.ConfiguracoesDeContasDeSistema.Remove(ConfiguracaoDeConta);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { contaId = ConfiguracaoDeConta.Conta_Id });
        }
    }
}

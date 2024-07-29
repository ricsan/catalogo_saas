using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.ContaRoot.Configuracoes
{
    public class IndexModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public IndexModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IList<ConfiguracaoDeConta> ConfiguracaoDeConta { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? contaId)
        {
            if (contaId is null || contaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(contaId)] = contaId;

            if (_context.ConfiguracoesDeContasDeSistema != null)
            {
                ConfiguracaoDeConta = await _context.ConfiguracoesDeContasDeSistema
                    .Include(c => c.Conta)
                        .ThenInclude(r => r.Tenant)
                    .Include(c => c.Parametro)
                    .Where(r => r.Conta_Id == contaId)
                    .ToListAsync();
            }

            return Page();
        }
    }
}

using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.ContaRoot.Configuracoes
{
    public class CreateModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public CreateModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? contaId)
        {
            if (contaId is null || contaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(contaId)] = contaId;

            ViewData["Parametro_Id"] = new SelectList(_context.ParametrosDeSistema, "Id", "CodigoUnico");
            //ViewData["Conta_Id"] = new SelectList(_context.ContasDeSistema, "Id", "Id");
            ViewData["Conta"] = _context.ContasDeSistema
                .Include(r => r.Tenant)
                .Where(r => r.Id == contaId)
                .Select(r => r.Id + " (" + r.Tenant.Nome + ")")
                .FirstOrDefault();

            return Page();
        }

        [BindProperty]
        public ConfiguracaoDeConta ConfiguracaoDeConta { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ConfiguracoesDeContasDeSistema == null || ConfiguracaoDeConta == null)
            {
                return Page();
            }

            _context.ConfiguracoesDeContasDeSistema.Add(ConfiguracaoDeConta);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { contaId = ConfiguracaoDeConta.Conta_Id });
        }
    }
}

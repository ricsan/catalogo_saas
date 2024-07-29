using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MD3.CatalogoSaaS.Adm.Pages.ContasRoot.Contas
{
    public class CreateModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public CreateModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Tenant_Id"] = new SelectList(_context.Tenants, "Id", "Nome");
            ViewData["Sistema_Id"] = new SelectList(_context.Sistemas, "Id", "Nome");
            ViewData["Plano_Id"] = new SelectList(
                _context.PlanosDeSistema.Select(r => new { Id = r.Id, Descricao = r.Descricao + " (" + r.Sistema.Nome + ")" }),
                "Id", "Descricao");
            return Page();
        }

        [BindProperty]
        public ContaDeSistema ContaDeSistema { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ContasDeSistema == null || ContaDeSistema == null)
            {
                return Page();
            }

            // Adiciona todos os parâmetros vazios do sistema selecionado
            var parametros = _context.ParametrosDeSistema.Where(r => r.Sistema_Id == ContaDeSistema.Sistema_Id && r.NivelDeConta).ToArray();
            foreach (var parametro in parametros)
            {
                ContaDeSistema.AdicionarConfiguracao(new ConfiguracaoDeConta(ContaDeSistema, parametro));
            }

            _context.ContasDeSistema.Add(ContaDeSistema);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

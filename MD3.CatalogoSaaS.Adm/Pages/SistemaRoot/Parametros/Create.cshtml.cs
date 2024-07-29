using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Parametros
{
    public class CreateModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public CreateModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? sistemaId)
        {
            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            //ViewData["Sistema_Id"] = new SelectList(_context.Sistemas, "Id", "CodigoInterno");
            ViewData["Sistema"] = _context.Sistemas.Where(r => r.Id == sistemaId).Select(r => r.Nome).FirstOrDefault();

            return Page();
        }

        [BindProperty]
        public ParametroDoSistema ParametroDoSistema { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ParametrosDeSistema == null || ParametroDoSistema == null)
            {
                return Page();
            }

            _context.ParametrosDeSistema.Add(ParametroDoSistema);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { sistemaId = ParametroDoSistema.Sistema_Id });
        }
    }
}

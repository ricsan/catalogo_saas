using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Sistemas
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
            return Page();
        }

        [BindProperty]
        public Sistema Sistema { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Sistemas == null || Sistema == null)
            {
                return Page();
            }

            _context.Sistemas.Add(Sistema);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

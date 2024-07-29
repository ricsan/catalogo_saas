using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Sistemas
{
    public class DeleteModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DeleteModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sistema Sistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sistemas == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistemas.FirstOrDefaultAsync(m => m.Id == id);

            if (sistema == null)
            {
                return NotFound();
            }
            else
            {
                Sistema = sistema;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sistemas == null)
            {
                return NotFound();
            }
            var sistema = await _context.Sistemas.FindAsync(id);

            if (sistema != null)
            {
                Sistema = sistema;
                _context.Sistemas.Remove(Sistema);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Sistemas
{
    public class DetailsModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DetailsModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

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
    }
}

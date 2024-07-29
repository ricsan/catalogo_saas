using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.TenantRoot.Tenants
{
    public class DeleteModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DeleteModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tenant Tenant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tenants == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.FirstOrDefaultAsync(m => m.Id == id);

            if (tenant == null)
            {
                return NotFound();
            }
            else
            {
                Tenant = tenant;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Tenants == null)
            {
                return NotFound();
            }
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant != null)
            {
                Tenant = tenant;
                _context.Tenants.Remove(Tenant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

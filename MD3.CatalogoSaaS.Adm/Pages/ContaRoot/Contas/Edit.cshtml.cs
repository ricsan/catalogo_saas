using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.ContasRoot.Contas
{
    public class EditModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public EditModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContaDeSistema ContaDeSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ContasDeSistema == null)
            {
                return NotFound();
            }

            var contadesistema = await _context.ContasDeSistema.FirstOrDefaultAsync(m => m.Id == id);
            if (contadesistema == null)
            {
                return NotFound();
            }
            ContaDeSistema = contadesistema;
            ViewData["Plano_Id"] = new SelectList(_context.PlanosDeSistema, "Id", "Descricao");
            ViewData["Sistema_Id"] = new SelectList(_context.Sistemas, "Id", "Nome");
            ViewData["Tenant_Id"] = new SelectList(_context.Tenants, "Id", "Nome");
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

            _context.Attach(ContaDeSistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaDeSistemaExists(ContaDeSistema.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContaDeSistemaExists(int? id)
        {
            return (_context.ContasDeSistema?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Planos
{
    public class EditModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public EditModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlanoDoSistema PlanoDoSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? sistemaId)
        {
            if (id == null || _context.PlanosDeSistema == null)
            {
                return NotFound();
            }

            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            var planodosistema = await _context.PlanosDeSistema.FirstOrDefaultAsync(m => m.Id == id);
            if (planodosistema == null)
            {
                return NotFound();
            }

            PlanoDoSistema = planodosistema;
            //ViewData["Sistema_Id"] = new SelectList(_context.Sistemas, "Id", "CodigoInterno");
            ViewData["Sistema"] = _context.Sistemas.Where(r => r.Id == sistemaId).Select(r => r.Nome).FirstOrDefault();

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

            _context.Attach(PlanoDoSistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanoDoSistemaExists(PlanoDoSistema.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { sistemaId = PlanoDoSistema.Sistema_Id });
        }

        private bool PlanoDoSistemaExists(int id)
        {
            return (_context.PlanosDeSistema?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

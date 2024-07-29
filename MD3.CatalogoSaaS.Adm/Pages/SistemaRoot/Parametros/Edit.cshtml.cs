using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Parametros
{
    public class EditModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public EditModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParametroDoSistema ParametroDoSistema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? sistemaId)
        {
            if (id == null || _context.ParametrosDeSistema == null)
            {
                return NotFound();
            }

            if (sistemaId is null || sistemaId == 0)
            {
                return NotFound();
            }
            ViewData[nameof(sistemaId)] = sistemaId;

            var parametrodosistema = await _context.ParametrosDeSistema.FirstOrDefaultAsync(m => m.Id == id);
            if (parametrodosistema == null)
            {
                return NotFound();
            }

            ParametroDoSistema = parametrodosistema;
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

            _context.Attach(ParametroDoSistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametroDoSistemaExists(ParametroDoSistema.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { sistemaId = ParametroDoSistema.Sistema_Id });
        }

        private bool ParametroDoSistemaExists(int? id)
        {
            return (_context.ParametrosDeSistema?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

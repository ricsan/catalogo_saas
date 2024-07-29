using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.UsuarioRoot.Usuarios
{
    public class DetailsModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public DetailsModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                Usuario = usuario;
            }
            return Page();
        }
    }
}

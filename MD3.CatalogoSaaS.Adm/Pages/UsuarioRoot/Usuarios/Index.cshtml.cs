using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.UsuarioRoot.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public IndexModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IList<Usuario> Usuario { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Usuarios != null)
            {
                Usuario = await _context.Usuarios.ToListAsync();
            }
        }
    }
}

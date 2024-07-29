using MD3.CatalogoSaaS.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MD3.CatalogoSaaS.Adm.Pages.SistemaRoot.Sistemas
{
    public class IndexModel : PageModel
    {
        private readonly MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext _context;

        public IndexModel(MD3.CatalogoSaaS.Data.EF.CatalogoSaaSContext context)
        {
            _context = context;
        }

        public IList<Sistema> Sistema { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Sistemas != null)
            {
                Sistema = await _context.Sistemas.ToListAsync();
            }
        }
    }
}

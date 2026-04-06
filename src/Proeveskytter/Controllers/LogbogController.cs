using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proeveskytter.Data;


namespace Proeveskytter.Controllers
{
    [Authorize]
    public class LogbogController : Controller
    {
        private readonly DatabaseContext _context;

        public LogbogController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Skytter
                .Include(s => s.Skydninger.OrderBy(sk => sk.Dato))
                .OrderBy(s => s.Navn)
               .ToListAsync());
        }
    }
}

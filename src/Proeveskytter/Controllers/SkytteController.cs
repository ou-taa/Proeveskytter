using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proeveskytter.Data;
using Proeveskytter.Models.Entities;

namespace Proeveskytter.Controllers
{
    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class SkytteController : Controller
    {
        private readonly DatabaseContext _context;

        public SkytteController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Skytte
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skytter
                .Include(s => s.Skydninger)
                .OrderBy(s => s.Navn)
                .ToListAsync());
        }

        // GET: Skytte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skytte = await _context.Skytter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skytte == null)
            {
                return NotFound();
            }

            return View(skytte);
        }

        // GET: Skytte/Create
        public IActionResult Create()
        {
            LoadIdTyper();
            return View();
        }

        // POST: Skytte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Navn,IdType,IdNr")] Skytte skytte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skytte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skytte);
        }

        // GET: Skytte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skytte = await _context.Skytter.FindAsync(id);
            if (skytte == null)
            {
                return NotFound();
            }
            LoadIdTyper();
            return View(skytte);
        }

        // POST: Skytte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Navn,IdType,IdNr")] Skytte skytte)
        {
            if (id != skytte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skytte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkytteExists(skytte.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(skytte);
        }

        // GET: Skytte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //return Unauthorized();

            if (id == null)
            {
                return NotFound();
            }

            var skytte = await _context.Skytter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skytte == null)
            {
                return NotFound();
            }

            return View(skytte);
        }

        // POST: Skytte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skytte = await _context.Skytter.FindAsync(id);
            if (skytte != null)
            {
                _context.Skytter.Remove(skytte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkytteExists(int id)
        {
            return _context.Skytter.Any(e => e.Id == id);
        }

        private void LoadIdTyper()
        {
            ViewBag.IdTyper = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Vælg" },
                new SelectListItem { Value = "1", Text = "Pas" },
                new SelectListItem { Value = "2", Text = "Kørekort" }
            };
        }
    }
}

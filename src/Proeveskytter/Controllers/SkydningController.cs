using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proeveskytter.Data;
using Proeveskytter.Models;

namespace Proeveskytter.Controllers
{
    [Authorize]
    public class SkydningController : Controller
    {
        private readonly DatabaseContext _context;

        public SkydningController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Skydning
        public async Task<IActionResult> Index(int id)
        {
            Skytte skytte= _context.Skytter
                .Include(s => s.Skydninger).OrderBy(s => s.Id)
                .Where(s => s.Id == id).Single();

            return View(skytte);
        }

        // GET: Skydning/Create
        public IActionResult Create(int id)
        {
            Skydning skydning = new Skydning();
            skydning.Skytte = _context.Skytter.Where(s => s.Id == id).Single();
            skydning.SkytteId = id;
            skydning.Dato = DateOnly.FromDateTime(DateTime.Now);
            return View(skydning);
        }

        // POST: Skydning/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Dato,SkytteId")] Skydning skydning)      
        {
            if (ModelState.IsValid)
            {
                _context.Add(skydning);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = skydning.SkytteId });
            }
            return View(skydning);
        }

       

        // GET: Skydning/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skydning = await _context.Skydninger
                .Include(s => s.Skytte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skydning == null)
            {
                return NotFound();
            }

            return View(skydning);
        }

        // POST: Skydning/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int skytteId)
        {
            var skydning = await _context.Skydninger.FindAsync(id);
            if (skydning != null)
            {
                _context.Skydninger.Remove(skydning);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = skytteId });
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STMIS.Data;
using STMIS.Models;

namespace STMIS.Controllers
{
    [Authorize]
    public class WeeksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeeksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weeks
        public async Task<IActionResult> Index()
        {
              return _context.WeeksTables != null ? 
                          View(await _context.WeeksTables.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.WeeksTables'  is null.");
        }

        // GET: Weeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WeeksTables == null)
            {
                return NotFound();
            }

            var weeksTable = await _context.WeeksTables
                .FirstOrDefaultAsync(m => m.WeekId == id);
            if (weeksTable == null)
            {
                return NotFound();
            }

            return View(weeksTable);
        }

        // GET: Weeks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekId,WeekName")] WeeksTable weeksTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weeksTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weeksTable);
        }

        // GET: Weeks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WeeksTables == null)
            {
                return NotFound();
            }

            var weeksTable = await _context.WeeksTables.FindAsync(id);
            if (weeksTable == null)
            {
                return NotFound();
            }
            return View(weeksTable);
        }

        // POST: Weeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeekId,WeekName")] WeeksTable weeksTable)
        {
            if (id != weeksTable.WeekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weeksTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeksTableExists(weeksTable.WeekId))
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
            return View(weeksTable);
        }

        // GET: Weeks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WeeksTables == null)
            {
                return NotFound();
            }

            var weeksTable = await _context.WeeksTables
                .FirstOrDefaultAsync(m => m.WeekId == id);
            if (weeksTable == null)
            {
                return NotFound();
            }

            return View(weeksTable);
        }

        // POST: Weeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WeeksTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WeeksTables'  is null.");
            }
            var weeksTable = await _context.WeeksTables.FindAsync(id);
            if (weeksTable != null)
            {
                _context.WeeksTables.Remove(weeksTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeeksTableExists(int id)
        {
          return (_context.WeeksTables?.Any(e => e.WeekId == id)).GetValueOrDefault();
        }
    }
}

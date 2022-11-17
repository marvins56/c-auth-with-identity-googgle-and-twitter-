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
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Class
        public async Task<IActionResult> Index()
        {
              return _context.ClassTables != null ? 
                          View(await _context.ClassTables.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ClassTables'  is null.");
        }

        // GET: Class/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassTables == null)
            {
                return NotFound();
            }

            var classTable = await _context.ClassTables
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (classTable == null)
            {
                return NotFound();
            }

            return View(classTable);
        }

        // GET: Class/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,ClassName")] ClassTable classTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classTable);
        }

        // GET: Class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassTables == null)
            {
                return NotFound();
            }

            var classTable = await _context.ClassTables.FindAsync(id);
            if (classTable == null)
            {
                return NotFound();
            }
            return View(classTable);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,ClassName")] ClassTable classTable)
        {
            if (id != classTable.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassTableExists(classTable.ClassId))
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
            return View(classTable);
        }

        // GET: Class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassTables == null)
            {
                return NotFound();
            }

            var classTable = await _context.ClassTables
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (classTable == null)
            {
                return NotFound();
            }

            return View(classTable);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClassTables'  is null.");
            }
            var classTable = await _context.ClassTables.FindAsync(id);
            if (classTable != null)
            {
                _context.ClassTables.Remove(classTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassTableExists(int id)
        {
          return (_context.ClassTables?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STMIS.Data;
using STMIS.Models;

namespace STMIS.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentsTables.Include(s => s.Class);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.StudentsTables == null)
            {
                return NotFound();
            }

            var studentsTable = await _context.StudentsTables
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentsTable == null)
            {
                return NotFound();
            }

            return View(studentsTable);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,ClassId")] StudentsTable studentsTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentsTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", studentsTable.ClassId);
            return View(studentsTable);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.StudentsTables == null)
            {
                return NotFound();
            }

            var studentsTable = await _context.StudentsTables.FindAsync(id);
            if (studentsTable == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", studentsTable.ClassId);
            return View(studentsTable);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,StudentName,ClassId")] StudentsTable studentsTable)
        {
            if (id != studentsTable.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentsTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsTableExists(studentsTable.StudentId))
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
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", studentsTable.ClassId);
            return View(studentsTable);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.StudentsTables == null)
            {
                return NotFound();
            }

            var studentsTable = await _context.StudentsTables
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentsTable == null)
            {
                return NotFound();
            }

            return View(studentsTable);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.StudentsTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentsTables'  is null.");
            }
            var studentsTable = await _context.StudentsTables.FindAsync(id);
            if (studentsTable != null)
            {
                _context.StudentsTables.Remove(studentsTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsTableExists(string id)
        {
          return (_context.StudentsTables?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}

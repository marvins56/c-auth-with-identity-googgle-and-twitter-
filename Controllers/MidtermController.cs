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
    public class MidtermController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MidtermController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Midterm
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MidtermMarksTables.Include(m => m.Class).Include(m => m.Student).Include(m => m.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Midterm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MidtermMarksTables == null)
            {
                return NotFound();
            }

            var midtermMarksTable = await _context.MidtermMarksTables
                .Include(m => m.Class)
                .Include(m => m.Student)
                .Include(m => m.Subject)
                .FirstOrDefaultAsync(m => m.MarksId == id);
            if (midtermMarksTable == null)
            {
                return NotFound();
            }

            return View(midtermMarksTable);
        }

        // GET: Midterm/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId");
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId");
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId");
            return View();
        }

        // POST: Midterm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarksId,SubjectId,StudentId,ClassId,Marks,Status")] MidtermMarksTable midtermMarksTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(midtermMarksTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", midtermMarksTable.ClassId);
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId", midtermMarksTable.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", midtermMarksTable.SubjectId);
            return View(midtermMarksTable);
        }

        // GET: Midterm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MidtermMarksTables == null)
            {
                return NotFound();
            }

            var midtermMarksTable = await _context.MidtermMarksTables.FindAsync(id);
            if (midtermMarksTable == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", midtermMarksTable.ClassId);
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId", midtermMarksTable.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", midtermMarksTable.SubjectId);
            return View(midtermMarksTable);
        }

        // POST: Midterm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarksId,SubjectId,StudentId,ClassId,Marks,Status")] MidtermMarksTable midtermMarksTable)
        {
            if (id != midtermMarksTable.MarksId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(midtermMarksTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MidtermMarksTableExists(midtermMarksTable.MarksId))
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
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", midtermMarksTable.ClassId);
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId", midtermMarksTable.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", midtermMarksTable.SubjectId);
            return View(midtermMarksTable);
        }

        // GET: Midterm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MidtermMarksTables == null)
            {
                return NotFound();
            }

            var midtermMarksTable = await _context.MidtermMarksTables
                .Include(m => m.Class)
                .Include(m => m.Student)
                .Include(m => m.Subject)
                .FirstOrDefaultAsync(m => m.MarksId == id);
            if (midtermMarksTable == null)
            {
                return NotFound();
            }

            return View(midtermMarksTable);
        }

        // POST: Midterm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MidtermMarksTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MidtermMarksTables'  is null.");
            }
            var midtermMarksTable = await _context.MidtermMarksTables.FindAsync(id);
            if (midtermMarksTable != null)
            {
                _context.MidtermMarksTables.Remove(midtermMarksTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MidtermMarksTableExists(int id)
        {
          return (_context.MidtermMarksTables?.Any(e => e.MarksId == id)).GetValueOrDefault();
        }
    }
}

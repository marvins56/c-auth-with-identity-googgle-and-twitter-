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
    public class EOTController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EOTController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EOT
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EndOftermMarksTables.Include(e => e.Class).Include(e => e.Student).Include(e => e.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EOT/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EndOftermMarksTables == null)
            {
                return NotFound();
            }

            var endOftermMarksTable = await _context.EndOftermMarksTables
                .Include(e => e.Class)
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.MarksId == id);
            if (endOftermMarksTable == null)
            {
                return NotFound();
            }

            return View(endOftermMarksTable);
        }

        // GET: EOT/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId");
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId");
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId");
            return View();
        }

        // POST: EOT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarksId,SubjectId,StudentId,ClassId,Marks,Status")] EndOftermMarksTable endOftermMarksTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(endOftermMarksTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", endOftermMarksTable.ClassId);
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId", endOftermMarksTable.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", endOftermMarksTable.SubjectId);
            return View(endOftermMarksTable);
        }

        // GET: EOT/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EndOftermMarksTables == null)
            {
                return NotFound();
            }

            var endOftermMarksTable = await _context.EndOftermMarksTables.FindAsync(id);
            if (endOftermMarksTable == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", endOftermMarksTable.ClassId);
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId", endOftermMarksTable.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", endOftermMarksTable.SubjectId);
            return View(endOftermMarksTable);
        }

        // POST: EOT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarksId,SubjectId,StudentId,ClassId,Marks,Status")] EndOftermMarksTable endOftermMarksTable)
        {
            if (id != endOftermMarksTable.MarksId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endOftermMarksTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EndOftermMarksTableExists(endOftermMarksTable.MarksId))
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
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", endOftermMarksTable.ClassId);
            ViewData["StudentId"] = new SelectList(_context.StudentsTables, "StudentId", "StudentId", endOftermMarksTable.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", endOftermMarksTable.SubjectId);
            return View(endOftermMarksTable);
        }

        // GET: EOT/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EndOftermMarksTables == null)
            {
                return NotFound();
            }

            var endOftermMarksTable = await _context.EndOftermMarksTables
                .Include(e => e.Class)
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.MarksId == id);
            if (endOftermMarksTable == null)
            {
                return NotFound();
            }

            return View(endOftermMarksTable);
        }

        // POST: EOT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EndOftermMarksTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EndOftermMarksTables'  is null.");
            }
            var endOftermMarksTable = await _context.EndOftermMarksTables.FindAsync(id);
            if (endOftermMarksTable != null)
            {
                _context.EndOftermMarksTables.Remove(endOftermMarksTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EndOftermMarksTableExists(int id)
        {
          return (_context.EndOftermMarksTables?.Any(e => e.MarksId == id)).GetValueOrDefault();
        }
    }
}

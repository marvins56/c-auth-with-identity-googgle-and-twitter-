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
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
              return _context.SubjectTables != null ? 
                          View(await _context.SubjectTables.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SubjectTables'  is null.");
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubjectTables == null)
            {
                return NotFound();
            }

            var subjectTable = await _context.SubjectTables
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subjectTable == null)
            {
                return NotFound();
            }

            return View(subjectTable);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Name")] SubjectTable subjectTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectTable);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubjectTables == null)
            {
                return NotFound();
            }

            var subjectTable = await _context.SubjectTables.FindAsync(id);
            if (subjectTable == null)
            {
                return NotFound();
            }
            return View(subjectTable);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,Name")] SubjectTable subjectTable)
        {
            if (id != subjectTable.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectTableExists(subjectTable.SubjectId))
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
            return View(subjectTable);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubjectTables == null)
            {
                return NotFound();
            }

            var subjectTable = await _context.SubjectTables
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subjectTable == null)
            {
                return NotFound();
            }

            return View(subjectTable);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubjectTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SubjectTables'  is null.");
            }
            var subjectTable = await _context.SubjectTables.FindAsync(id);
            if (subjectTable != null)
            {
                _context.SubjectTables.Remove(subjectTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectTableExists(int id)
        {
          return (_context.SubjectTables?.Any(e => e.SubjectId == id)).GetValueOrDefault();
        }
    }
}

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
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TopicsTables.Include(t => t.Class).Include(t => t.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TopicsTables == null)
            {
                return NotFound();
            }

            var topicsTable = await _context.TopicsTables
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topicsTable == null)
            {
                return NotFound();
            }

            return View(topicsTable);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId");
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId");
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicId,TopicName,ClassId,IsComplete,DateTime,SubjectId")] TopicsTable topicsTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicsTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", topicsTable.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", topicsTable.SubjectId);
            return View(topicsTable);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TopicsTables == null)
            {
                return NotFound();
            }

            var topicsTable = await _context.TopicsTables.FindAsync(id);
            if (topicsTable == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", topicsTable.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", topicsTable.SubjectId);
            return View(topicsTable);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicId,TopicName,ClassId,IsComplete,DateTime,SubjectId")] TopicsTable topicsTable)
        {
            if (id != topicsTable.TopicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicsTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicsTableExists(topicsTable.TopicId))
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
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", topicsTable.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.SubjectTables, "SubjectId", "SubjectId", topicsTable.SubjectId);
            return View(topicsTable);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TopicsTables == null)
            {
                return NotFound();
            }

            var topicsTable = await _context.TopicsTables
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topicsTable == null)
            {
                return NotFound();
            }

            return View(topicsTable);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TopicsTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TopicsTables'  is null.");
            }
            var topicsTable = await _context.TopicsTables.FindAsync(id);
            if (topicsTable != null)
            {
                _context.TopicsTables.Remove(topicsTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicsTableExists(int id)
        {
          return (_context.TopicsTables?.Any(e => e.TopicId == id)).GetValueOrDefault();
        }
    }
}

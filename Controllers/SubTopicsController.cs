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
    public class SubTopicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubTopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubTopics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubTopicsTables.Include(s => s.Class).Include(s => s.Topic);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubTopicsTables == null)
            {
                return NotFound();
            }

            var subTopicsTable = await _context.SubTopicsTables
                .Include(s => s.Class)
                .Include(s => s.Topic)
                .FirstOrDefaultAsync(m => m.SubTopicsId == id);
            if (subTopicsTable == null)
            {
                return NotFound();
            }

            return View(subTopicsTable);
        }

        // GET: SubTopics/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId");
            ViewData["TopicId"] = new SelectList(_context.TopicsTables, "TopicId", "TopicId");
            return View();
        }

        // POST: SubTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubTopicsId,TopicId,SubTopic,Overview,IsComplete,Datetime,ClassId")] SubTopicsTable subTopicsTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subTopicsTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", subTopicsTable.ClassId);
            ViewData["TopicId"] = new SelectList(_context.TopicsTables, "TopicId", "TopicId", subTopicsTable.TopicId);
            return View(subTopicsTable);
        }

        // GET: SubTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubTopicsTables == null)
            {
                return NotFound();
            }

            var subTopicsTable = await _context.SubTopicsTables.FindAsync(id);
            if (subTopicsTable == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", subTopicsTable.ClassId);
            ViewData["TopicId"] = new SelectList(_context.TopicsTables, "TopicId", "TopicId", subTopicsTable.TopicId);
            return View(subTopicsTable);
        }

        // POST: SubTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubTopicsId,TopicId,SubTopic,Overview,IsComplete,Datetime,ClassId")] SubTopicsTable subTopicsTable)
        {
            if (id != subTopicsTable.SubTopicsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subTopicsTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubTopicsTableExists(subTopicsTable.SubTopicsId))
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
            ViewData["ClassId"] = new SelectList(_context.ClassTables, "ClassId", "ClassId", subTopicsTable.ClassId);
            ViewData["TopicId"] = new SelectList(_context.TopicsTables, "TopicId", "TopicId", subTopicsTable.TopicId);
            return View(subTopicsTable);
        }

        // GET: SubTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubTopicsTables == null)
            {
                return NotFound();
            }

            var subTopicsTable = await _context.SubTopicsTables
                .Include(s => s.Class)
                .Include(s => s.Topic)
                .FirstOrDefaultAsync(m => m.SubTopicsId == id);
            if (subTopicsTable == null)
            {
                return NotFound();
            }

            return View(subTopicsTable);
        }

        // POST: SubTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubTopicsTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SubTopicsTables'  is null.");
            }
            var subTopicsTable = await _context.SubTopicsTables.FindAsync(id);
            if (subTopicsTable != null)
            {
                _context.SubTopicsTables.Remove(subTopicsTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubTopicsTableExists(int id)
        {
          return (_context.SubTopicsTables?.Any(e => e.SubTopicsId == id)).GetValueOrDefault();
        }
    }
}

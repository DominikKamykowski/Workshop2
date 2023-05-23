using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Workshop2.Models;

namespace Workshop2.Controllers
{
    public class WorkTasksController : Controller
    {
        private readonly AppDbContext _context;

        public WorkTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WorkTasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tasks.Include(w => w.Car).Where(x => x.IsCompleted == false);
            return View(await appDbContext.ToListAsync());
        }

        // GET: WorkTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var workTask = await _context.Tasks
                .Include(w => w.Car)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // GET: WorkTasks/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", nameof(Car.CarFullName));
            return View();
        }

        // POST: WorkTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,CarId,TaskStartDate,TaskEndDate,Description,IsCompleted")] WorkTask workTask)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(workTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", workTask.CarId);
            return View(workTask);
        }

        // GET: WorkTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var workTask = await _context.Tasks.FindAsync(id);              
            if (workTask == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", nameof(Car.CarFullName), workTask.CarId);
            return View(workTask);
        }

        // POST: WorkTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,CarId,TaskStartDate,TaskEndDate,Description,IsCompleted")] WorkTask workTask)
        {
            if (id != workTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTaskExists(workTask.TaskId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", workTask.CarId);
            return View(workTask);
        }

        // GET: WorkTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var workTask = await _context.Tasks
                .Include(w => w.Car)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // POST: WorkTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'AppDbContext.Tasks'  is null.");
            }
            var workTask = await _context.Tasks.FindAsync(id);
            if (workTask != null)
            {
                _context.Tasks.Remove(workTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTaskExists(int id)
        {
          return (_context.Tasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Completed(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var workTask = await _context.Tasks
                .Include(w => w.Car)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (workTask == null)
            {
                return NotFound();
            }
            else
            {
                workTask.IsCompleted = true;
                workTask.TaskEndDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AllList()
        {
            var appDbContext = _context.Tasks.Include(w => w.Car).Where(x => x.IsCompleted == true);
            return View(await appDbContext.ToListAsync());
        }
    }
}

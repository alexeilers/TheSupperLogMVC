using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;

namespace TheSupperLog.Controllers
{
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rating
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ratings.Include(r => r.Meal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rating/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingEntity = await _context.Ratings
                .Include(r => r.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingEntity == null)
            {
                return NotFound();
            }

            return View(ratingEntity);
        }

        // GET: Rating/Create
        public IActionResult Create()
        {
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name");
            return View();
        }

        // POST: Rating/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MealId,Rating,DateAdded,DateModified")] RatingEntity ratingEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratingEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", ratingEntity.MealId);
            return View(ratingEntity);
        }

        // GET: Rating/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingEntity = await _context.Ratings.FindAsync(id);
            if (ratingEntity == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", ratingEntity.MealId);
            return View(ratingEntity);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MealId,Rating,DateAdded,DateModified")] RatingEntity ratingEntity)
        {
            if (id != ratingEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingEntityExists(ratingEntity.Id))
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
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", ratingEntity.MealId);
            return View(ratingEntity);
        }

        // GET: Rating/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingEntity = await _context.Ratings
                .Include(r => r.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ratingEntity == null)
            {
                return NotFound();
            }

            return View(ratingEntity);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingEntity = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(ratingEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingEntityExists(int id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }
    }
}

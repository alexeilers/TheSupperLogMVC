using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.Meal;

namespace TheSupperLog.Controllers
{
    public class MealController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MealController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Meal
        public async Task<IActionResult> Index(string searchString) {

        //public async Task<IActionResult> Index()
        //{
        //var meals = await _context
        //    .Meals
        //    .Select(m => new MealListItem
        //    {
        //        Id = m.Id,
        //        Name = m.Name,
        //        Rating = m.Rating,
        //        DateAdded = m.DateAdded,
        //    })
        //    .ToListAsync();
        //return View(meals);


        //SEARCH Meal
        var meals = from m in _context.Meals
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    meals = meals.Where(s => s.Name!.Contains(searchString));
                }

                return View(await meals.ToListAsync());
        }

        // GET: Meal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntity = await _context.Meals
                .Select(m => new MealDetail
                {
                    Id = m.Id,
                    Name = m.Name,
                    Rating = m.Rating,
                    DateAdded = m.DateAdded,
                })
                .FirstOrDefaultAsync(r => r.Id == id);
            if (mealEntity == null)
            {
                return NotFound();
            }

            return View(mealEntity);
        }

        // GET: Meal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Rating")] MealCreate model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new MealEntity
                {
                    OwnerId = 1,
                    Name = model.Name,
                    Rating = model.Rating,
                    DateAdded = DateTimeOffset.Now,
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        // GET: Meal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntity = await _context.Meals.Select(m => new MealEdit
            {
                Id = m.Id,
                Name = m.Name,
                Rating = m.Rating,
            })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealEntity == null)
            {
                return NotFound();
            }

            return View(mealEntity);
        }

        // POST: Meal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,Name,Rating,DateAdded,DateModified")] MealEdit mealEntity)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                meal.Id = mealEntity.Id;
                meal.Name = mealEntity.Name;
                meal.Rating = mealEntity.Rating;
                meal.DateModified = DateTimeOffset.Now;
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealEntityExists(meal.Id))
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
            return View (meal);
        }

        // GET: Meal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context
                .Meals
                .Select(m => new MealDetail
                {
                    Id = m.Id,
                    Name = m.Name,
                    Rating = m.Rating
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mealEntity = await _context.Meals.FindAsync(id);
            _context.Meals.Remove(mealEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealEntityExists(int id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }
    }
}
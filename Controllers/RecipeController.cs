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
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recipe
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recipes.Include(r => r.Meal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeEntity = await _context.Recipes
                .Include(r => r.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeEntity == null)
            {
                return NotFound();
            }

            return View(recipeEntity);
        }

        // GET: Recipe/Create
        public IActionResult Create()
        {
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name");
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MealId,Name,Category,Yield,PrepTime,CookTime,TotalTime,Instructions,DateAdded,DateModified")] RecipeEntity recipeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", recipeEntity.MealId);
            return View(recipeEntity);
        }

        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeEntity = await _context.Recipes.FindAsync(id);
            if (recipeEntity == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", recipeEntity.MealId);
            return View(recipeEntity);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MealId,Name,Category,Yield,PrepTime,CookTime,TotalTime,Instructions,DateAdded,DateModified")] RecipeEntity recipeEntity)
        {
            if (id != recipeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeEntityExists(recipeEntity.Id))
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
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", recipeEntity.MealId);
            return View(recipeEntity);
        }

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeEntity = await _context.Recipes
                .Include(r => r.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeEntity == null)
            {
                return NotFound();
            }

            return View(recipeEntity);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeEntity = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeEntityExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}

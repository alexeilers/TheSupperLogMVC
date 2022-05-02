using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.Recipe;

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
        public async Task<IActionResult> Index(string searchString)
        {
            var recipes = from r in _context.Recipes
                        select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Name!.Contains(searchString));
            }

            return View(await recipes.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,MealId,Name,Category,Yield,PrepTime,CookTime,TotalTime,Instructions,DateAdded,DateModified")] RecipeEntity model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new RecipeEntity
                {
                    MealId = model.MealId,
                    Name = model.Name,
                    Category = model.Category,
                    Yield = model.Yield,
                    PrepTime = model.PrepTime,
                    CookTime = model.CookTime,
                    TotalTime = model.TotalTime,
                    Instructions = model.Instructions,
                    DateAdded = DateTimeOffset.Now,
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context
                .Recipes
                .Select(r => new RecipeEdit
                {
                    Id = r.Id,
                    MealId = r.MealId,
                    Name = r.Name,

                })
                .FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Category,Yield,PrepTime,CookTime,TotalTime,Instructions,DateAdded,DateModified")] RecipeEdit model)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                recipe.MealId = model.MealId;
                recipe.Name = model.Name;
                recipe.Category = model.Category;
                recipe.PrepTime = model.PrepTime;
                recipe.CookTime = model.CookTime;
                recipe.TotalTime = model.TotalTime;
                recipe.Instructions = model.Instructions;
                
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeEntityExists(recipe.Id))
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
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", recipe.MealId);
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context
                .Recipes
                .Select(m => new RecipeDetail
                {
                    Id = m.Id,
                    Name = m.Name,
                    
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
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

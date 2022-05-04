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
using TheSupperLog.Services.Recipe;

namespace TheSupperLog.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }


        // GET: Recipe
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();

            return View(recipes);
        }


        //// GET: Recipe/Details/5
        //public IActionResult Details()
        //{
        //    return View();
        //}


        //// GET: Recipe/Details/5
        //public async Task<IActionResult> DetailsById(int id)
        //{
        //    var recipe = await _recipeService.GetRecipeByIdAsync(id);

        //    if (recipe == null)
        //    {
        //        return null;
        //    }
        //    return View(recipe);
        //}


        // GET: Recipe/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Recipe/Create
        [HttpPost]
        public async Task<ActionResult> Create(RecipeCreate model)
        {
            if (model == null) return BadRequest();

            bool wasSuccess = await _recipeService.CreateRecipeAsync(model);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return UnprocessableEntity();
        }


        //GET EDIT Meal/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var recipeQuery = await _recipeService.GetRecipeByIdAsync(id);

            var recipe = new RecipeEdit();

            recipe.Id = recipeQuery.Id;
            recipe.Name = recipeQuery.Name;
            recipe.Category = recipeQuery.Category;
            recipe.PrepTime = recipeQuery.PrepTime;
            recipe.CookTime = recipeQuery.CookTime;
            recipe.TotalTime = recipeQuery.TotalTime;
            recipe.Instructions = recipeQuery.Instructions;
            recipe.DateModified = recipeQuery.DateModified;
           
            return View(recipe);
        }


        // POST: Recipe/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEdit model)
        {
            bool wasSuccess = await _recipeService.UpdateRecipeAsync(model);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }


        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var recipeQuery = await _recipeService.GetRecipeByIdAsync(id);

            var recipe = new RecipeDetail();

            recipe.Id = recipeQuery.Id;
            recipe.Name = recipeQuery.Name;
            recipe.Category = recipeQuery.Category;
            recipe.PrepTime = recipeQuery.PrepTime;
            recipe.CookTime = recipeQuery.CookTime;
            recipe.TotalTime = recipeQuery.TotalTime;
            recipe.Instructions = recipeQuery.Instructions;
            recipe.DateModified = recipeQuery.DateModified;

            return View(recipe);
        }


        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool wasSuccess = await _recipeService.DeleteRecipeAsync(id);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
    }
}

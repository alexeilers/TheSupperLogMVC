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

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return null;
            }
            return View(recipe);
        }


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
                return Ok();
            }

            else return UnprocessableEntity();
        }


        //GET EDIT Meal/Edit/5
        public IActionResult Edit()
        {
            return View();
        }


        // POST: Recipe/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEdit model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();

            bool wasSuccess = await _recipeService.UpdateRecipeAsync(model);

            if (wasSuccess) return Ok();

            return BadRequest();
        }


        // GET: Recipe/Delete/5
        public IActionResult Delete()
        {
            return View();
        }


        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int recipeId)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            bool wasSuccess = await _recipeService.DeleteRecipeAsync(recipeId);

            if (!wasSuccess) return BadRequest();

            return Ok();
        }
    }
}

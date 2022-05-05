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
using TheSupperLog.Services;
using TheSupperLog.Services.Meal;

namespace TheSupperLog.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {

            _mealService = mealService;
        }

        //INDEX
        public async Task<IActionResult> Index(string searchString)
        {
            var meals = await _mealService.GetAllMealsAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                meals = meals.Where(s => s.Name!.ToLower().Contains(searchString.ToLower()));
            }

            return View(meals.OrderByDescending(meals => meals.DateAdded).Take(10).ToList());
        }


        // GET: Favorites
        public async Task<IActionResult> Favorites()
        {
            var meals = await _mealService.GetAllMealsAsync();

            return View(meals.OrderByDescending(meals => meals.Rating).Take(10).ToList());
        }


        // GET: Meal/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);

            if (meal == null)
            {
                return null;
            }
            return View(meal);
        }

        // GET: Meal/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Meal/Create
        [HttpPost]
        public async Task<ActionResult> Create(MealCreate model)
        {
            if (model == null) return BadRequest();

            bool wasSuccess = await _mealService.CreateMealAsync(model);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            else return UnprocessableEntity();
        }

        //GET EDIT Meal/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var mealQuery = await _mealService.GetMealByIdAsync(id);

            var meal = new MealEdit();

            meal.Id = mealQuery.Id;
            meal.Name = mealQuery.Name;
            meal.Rating = mealQuery.Rating;

            return View(meal);
        }


        // POST: Meal/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(MealEdit model)
        {
            bool wasSuccess = await _mealService.UpdateMealAsync(model);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            else return BadRequest();
        }


        // GET: Meal/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var mealQuery = await _mealService.GetMealByIdAsync(id);

            var meal = new MealDetail();

            meal.Id = mealQuery.Id;
            meal.Name = mealQuery.Name;
            meal.Rating = mealQuery.Rating;

            return View(meal);
        }



        // POST: Meal/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool wasSuccess = await _mealService.DeleteMealAsync(id);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            else return BadRequest();
        }
    }
}
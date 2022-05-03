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

        // GET: Meal
        public async Task<IActionResult> Index()
        {
            var meals = await _mealService.GetAllMealsAsync();

            return View(meals);
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
                return Ok();
            }

            else return UnprocessableEntity();
        }

        //GET EDIT Meal/Edit/5
        public IActionResult Edit()
        {
            return View();
        }


        // POST: Meal/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(MealEdit model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();

            bool wasSuccess = await _mealService.UpdateMealAsync(model);

            if (wasSuccess) return Ok();

            return BadRequest();
        }


        // GET: Meal/Delete/5
        public IActionResult Delete()
        {
            return View();
        }



        // POST: Meal/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> Delete(int mealId)
        {
            var productEntity = await _mealService.GetMealByIdAsync(mealId);

            if (productEntity == null)
            {
                return NotFound();
            }

            bool wasSuccess = await _mealService.DeleteMealAsync(mealId);

            if (!wasSuccess) return BadRequest();

            return Ok();
        }
    }
}


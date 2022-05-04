using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.Meal;

namespace TheSupperLog.Services.Meal
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext _context;

        public MealService(ApplicationDbContext context)
        {
            _context = context;
        }

        //CREATE MEAL
        public async Task<bool> CreateMealAsync(MealCreate model)
        {
            var mealEntity = new MealEntity
            {
                Name = model.Name,
                Rating = model.Rating,
                DateAdded = DateTimeOffset.Now,
                OwnerId = 1
            };

            _context.Meals.Add(mealEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }


        //DELETE MEAL
        public async Task<bool> DeleteMealAsync(int mealId)
        {
            var mealEntity = await _context.Meals.FindAsync(mealId);

            _context.Meals.Remove(mealEntity);
            return await _context.SaveChangesAsync() == 1;
        }


        //GET ALL MEALS
        public async Task<IEnumerable<MealListItem>> GetAllMealsAsync()
        {
            var mealQuery = _context
                .Meals
                .Select(m =>
                new MealListItem
                {
                    Id = m.Id,
                    Name = m.Name,
                    Rating = m.Rating,
                    OwnerId = m.OwnerId,
                    DateAdded = m.DateAdded
                });
            return await mealQuery.ToListAsync();
        }

        //GET MEAL BY ID
        public async Task<MealDetail> GetMealByIdAsync(int mealId)
        {
            var model = await _context.Meals.FirstOrDefaultAsync(m => m.Id == mealId);

            if (model == null) return null;

            var meal = new MealDetail
            {
                Name = model.Name,
                Rating = model.Rating,
            };

            return meal;
        }


        //GET MEAL BY NAME
        public async Task<MealDetail> GetMealByNameAsync(string name)
        {
            var meal = await _context.Meals
                .FirstOrDefaultAsync(m =>
                    m.Name == name
                );
            return meal is null ? null : new MealDetail
            {
                Id = meal.Id,
                Name = meal.Name,
                Rating = meal.Rating
            };
        }


        //UPDATE MEAL
        public async Task<bool> UpdateMealAsync(MealEdit model)
        {
            var mealEntity = await _context.Meals.FindAsync(model.Id);

            mealEntity.Name = model.Name;
            mealEntity.Rating = model.Rating;
            mealEntity.DateModified = DateTimeOffset.Now;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.Meal;

namespace TheSupperLog.Services
{
    public class MealService : IMealService
    {
        private readonly ApplicationDbContext _context;

        public MealService(ApplicationDbContext context)
        {
            _context = context;
        }


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

        public async Task<bool> DeleteMealAsync(int mealId)
        {
            var mealEntity = await _context.Meals.FindAsync(mealId);

            _context.Meals.Remove(mealEntity);
            return await _context.SaveChangesAsync() == 1; ;
        }

    

        public async Task<IEnumerable<MealListItem>> GetAllMealsAsync()
        {
            var mealQuery = _context
                .Meals
                .Select(m =>
                new MealListItem
                {
                    Id = m.Id,
                    Name = m.Name,
                    DateAdded = m.DateAdded
                });
            return await mealQuery.ToListAsync();
        }

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

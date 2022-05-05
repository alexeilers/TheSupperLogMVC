using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSupperLog.Models.Meal;

namespace TheSupperLog.Services
{
    public interface IMealService
    {
        Task<IEnumerable<MealListItem>> GetAllMealsAsync();
        Task<bool> CreateMealAsync(MealCreate model);
        Task<MealDetail> GetMealByIdAsync(int mealId);
        Task<bool> UpdateMealAsync(MealEdit model);
        Task<bool> DeleteMealAsync(int mealId);
    }
}

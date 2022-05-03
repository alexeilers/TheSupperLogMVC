using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSupperLog.Models.Recipe;

namespace TheSupperLog.Services.Recipe
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeListItem>> GetAllRecipesAsync();
        Task<bool> CreateRecipeAsync(RecipeCreate model);
        Task<RecipeDetail> GetRecipeByIdAsync(int id);
        Task<bool> UpdateRecipeAsync(RecipeEdit model);
        Task<bool> DeleteRecipeAsync(int recipeId);
    }
}

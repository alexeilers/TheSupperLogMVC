using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.Recipe;

namespace TheSupperLog.Services.Recipe
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRecipeAsync(RecipeCreate model)
        {
            var recipeEntity = new RecipeEntity
            {
                Name = model.Name,
                MealId = model.MealId,
                Category = model.Category,
                Yield = model.Yield,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                TotalTime = model.TotalTime,
                Instructions = model.Instructions,
            };

            _context.Recipes.Add(recipeEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteRecipeAsync(int recipeId)
        {
            var recipeEntity = await _context.Meals.FindAsync(recipeId);

            _context.Meals.Remove(recipeEntity);
            return await _context.SaveChangesAsync() == 1; ;
        }

        public async Task<IEnumerable<RecipeListItem>> GetAllRecipesAsync()
        {
            var recipeQuery = _context
                .Recipes
                .Select(m =>
                new RecipeListItem
                {
                    Id = m.Id,
                    Name = m.Name,
                    DateAdded = m.DateAdded
                });
            return await recipeQuery.ToListAsync();
        }

        //public async Task<RecipeDetail> GetRecipeByNameAsync(string name)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> UpdateRecipeAsync(RecipeEdit model)
        {
            var recipeEntity = await _context.Recipes.FindAsync(model.Id);

            recipeEntity.Name = model.Name;
            recipeEntity.Category = model.Category;
            recipeEntity.Yield = model.Yield;
            recipeEntity.PrepTime = model.PrepTime;
            recipeEntity.CookTime = model.CookTime;
            recipeEntity.TotalTime = model.TotalTime;
            recipeEntity.Instructions = model.Instructions;
            recipeEntity.DateModified = DateTimeOffset.Now;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}

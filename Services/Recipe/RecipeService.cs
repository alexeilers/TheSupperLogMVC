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



        //CREATE RECIPE
        public async Task<bool> CreateRecipeAsync(RecipeCreate model)
        {
            var recipeEntity = new RecipeEntity
            {
                Name = model.Name,
                Category = model.Category,
                Yield = model.Yield,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                TotalTime = model.TotalTime,
                Instructions = model.Instructions,
                DateAdded = DateTimeOffset.Now
            };

            _context.Recipes.Add(recipeEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }




        //DELETE RECIPE
        public async Task<bool> DeleteRecipeAsync(int recipeId)
        {
            var recipeEntity = await _context.Recipes.FindAsync(recipeId);

            _context.Recipes.Remove(recipeEntity);
            return await _context.SaveChangesAsync() == 1;
        }



        //GET ALL RECIPES
        public async Task<IEnumerable<RecipeListItem>> GetAllRecipesAsync()
        {
            var recipeQuery = _context
                .Recipes
                .Select(m =>
                new RecipeListItem
                {
                    Id = m.Id,
                    Name = m.Name,
                    Category = m.Category,
                    DateAdded = m.DateAdded
                });
            return await recipeQuery.ToListAsync();
        }



        //GET RECIPE BY ID
        public async Task<RecipeDetail> GetRecipeByIdAsync(int recipeId)
        {
            var model = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == recipeId);

            if (model == null) return null;

            var recipe = new RecipeDetail
            {
                Id = model.Id,
                Name = model.Name,
                Category = model.Category,
                Yield = model.Yield,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                TotalTime = model.CookTime,
                Instructions = model.Instructions,
                DateAdded = model.DateAdded,
                DateModified = model.DateModified,
            };

            return recipe;
        }



        //EDIT RECIPE
        public async Task<bool> UpdateRecipeAsync(RecipeEdit model)
        {
            var recipeEntity = await _context.Recipes.FindAsync(model.Id);

            recipeEntity.Id = model.Id;
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

using System;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data.Entities;

namespace TheSupperLog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MealEntity> Meals { get; set; }
        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
    }
}

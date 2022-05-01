using System;
namespace TheSupperLog.Models.Recipe
{
    public class RecipeListItem
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Yield { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int TotalTime { get; set; }
        public string Instructions { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}

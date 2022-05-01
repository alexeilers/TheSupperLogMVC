using System;
namespace TheSupperLog.Models.Meal
{
    public class MealDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}

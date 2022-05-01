using System;
namespace TheSupperLog.Models.Meal
{
    public class MealListItem
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
}

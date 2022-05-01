using System;
namespace TheSupperLog.Models.Rating
{
    public class RatingDetail
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset DateAdded{ get; set; }
    }
}

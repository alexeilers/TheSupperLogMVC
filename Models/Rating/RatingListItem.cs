using System;
namespace TheSupperLog.Models.Rating
{
    public class RatingListItem
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int Rating { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace TheSupperLog.Models.Rating
{
    public class RatingCreate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int MealId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between {1} and {2}")]
        public int Rating { get; set; }

        public DateTimeOffset DateAdded{ get; set; }
    }
}

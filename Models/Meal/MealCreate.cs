using System;
using System.ComponentModel.DataAnnotations;

namespace TheSupperLog.Models.Meal
{
    public class MealCreate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace TheSupperLog.Models.Recipe
{
    public class RecipeCreate
    {
        //[Required]
        //public int MealId { get; set; }


        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }
        public int Yield { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int TotalTime { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(4000, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Instructions { get; set; }

    }
}

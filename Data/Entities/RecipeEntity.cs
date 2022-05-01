using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheSupperLog.Data.Entities
{
    public class RecipeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }
        public MealEntity Meal { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }
        public int Yield { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int TotalTime { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}

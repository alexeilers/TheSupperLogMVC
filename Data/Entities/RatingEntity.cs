using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheSupperLog.Data.Entities
{
    public class RatingEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }
        public MealEntity Meal { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}

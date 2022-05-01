using System;
using System.ComponentModel.DataAnnotations;

namespace TheSupperLog.Models.Rating
{
    public class RatingEdit
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between {1} and {2}")]
        public int Rating { get; set; }
    }
}

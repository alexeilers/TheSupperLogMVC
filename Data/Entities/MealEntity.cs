using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheSupperLog.Data.Entities
{
    public class MealEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public UserEntity Owner { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}
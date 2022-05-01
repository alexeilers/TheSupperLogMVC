using System;
using System.ComponentModel.DataAnnotations;

namespace TheSupperLog.Data.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter an e-mail address,")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}

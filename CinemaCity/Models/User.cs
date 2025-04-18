using System.ComponentModel.DataAnnotations;

namespace CinemaCity.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string ImageUrl { get; set; }

    }
}

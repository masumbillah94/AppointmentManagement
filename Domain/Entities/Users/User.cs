
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Users
{
    public class User:BaseEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Password { get; set; } = string.Empty; 
    }
}

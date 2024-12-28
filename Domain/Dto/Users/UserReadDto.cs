using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.Users
{
    public class UserReadDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

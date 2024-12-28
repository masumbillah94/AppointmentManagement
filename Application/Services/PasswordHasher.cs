using Domain.Abstractions.Base;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private static readonly string SecretKey = "qIeJsWzAxCnHyRmFtGbVdPoKlNuMqRsX";
        public string HashPassword(string userName, string plainPassword)
        {
            var storedPassword = SecretKey + plainPassword + userName;
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(storedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}

using Domain.Abstractions.Base;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        // This should be a fixed secret key (need to keep it secret, store securely but for demo purpose I keedp It here)
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

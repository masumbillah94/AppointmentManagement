using System;
namespace Domain.Abstractions.Base
{
    public interface IPasswordHasher
    {
        string HashPassword(string userName, string plainPassword);
    }
}

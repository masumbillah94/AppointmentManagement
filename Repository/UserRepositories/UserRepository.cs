using Data.SqlServer.AppContext;
using Domain.Abstractions.Users;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Repository.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
        {
            if (await _context.Users.AnyAsync(u => u.Username == user.Username, cancellationToken))
                throw new Exception("Username already exists!");

            _context.Users.Add(user);
        }

        public async Task<User> GetUserByUserName(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName, cancellationToken);
            return user!;
        }
    }
}

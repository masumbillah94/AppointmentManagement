using Domain.Entities.Appointments;
using Domain.Entities.Doctors;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

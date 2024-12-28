
using Domain.Abstractions.Appointments;
using Domain.Abstractions.Base;
using Domain.Abstractions.Users;
using Microsoft.Extensions.DependencyInjection;
using Repository.AppointmentRepositories;
using Repository.Base;
using Repository.UserRepositories;
namespace Repository
{
    public static class RepositoryServices
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryFacade, RepositoryFacade>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

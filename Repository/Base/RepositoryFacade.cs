using Data.SqlServer.AppContext;
using Domain.Abstractions.Appointments;
using Domain.Abstractions.Base;
using Domain.Abstractions.Users;

namespace Repository.Base
{
    public class RepositoryFacade : IRepositoryFacade
    {
        #region Private Fields

        private readonly AppDbContext Context;

        #endregion Private Fields

        #region Public Constructors

        public RepositoryFacade(
            AppDbContext context,
            IAppointmentRepository appointmentRepository,
            IUserRepository userRepository
        )
        {
            Context = context;
            AppointmentRepo = appointmentRepository;
            UserRepo = userRepository;

        }

        #endregion Public Constructors

        #region Public Properties

        public IAppointmentRepository AppointmentRepo { get; }
        public IUserRepository UserRepo { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }



        #endregion Public Properties

    }
}

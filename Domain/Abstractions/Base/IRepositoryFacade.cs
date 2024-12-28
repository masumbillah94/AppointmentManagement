using Domain.Abstractions.Appointments;
using Domain.Abstractions.Users;

namespace Domain.Abstractions.Base
{
    public interface IRepositoryFacade
    {
        #region Public Properties

        IAppointmentRepository AppointmentRepo { get; }
        IUserRepository UserRepo { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Public Properties
    }
}

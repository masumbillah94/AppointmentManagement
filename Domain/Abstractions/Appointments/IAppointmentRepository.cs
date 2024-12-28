
using Domain.Dto.Appointments;
using Domain.Dto.Common;
using Domain.Entities.Appointments;

namespace Domain.Abstractions.Appointments
{
    public interface IAppointmentRepository
    {
        #region Public Methods

        Task<Appointment> AddEntityAsync(Appointment entity, CancellationToken cancellationToken = default);
        Task<PaginatedResult<AppointmentReadDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<AppointmentReadDto> GetByIdAsync(long Id, CancellationToken cancellationToken = default);
        Task<Appointment> UpdateEntityAsync(Appointment entity, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default);

        #endregion Public Methods
    }
}

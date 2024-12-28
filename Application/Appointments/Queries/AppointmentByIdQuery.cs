using Application.Common.Models;
using Domain.Dto.Appointments;
using MediatR;

namespace Application.Appointments.Queries
{
    public class AppointmentByIdQuery : IRequest<ResponseDetail<AppointmentReadDto>>
    {
        #region Public Properties

        public long Id { get; set; }

        #endregion Public Properties
    }
}

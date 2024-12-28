using Application.Common.Models;
using Domain.Dto.Appointments;
using MediatR;

namespace Application.Appointments.Commands
{
    public class AppointmentDeleteCommand : IRequest<ResponseDetail<AppointmentReadDto>>
    {
        #region Public Properties

        public long Id { get; set; }

        #endregion Public Properties
    }
}

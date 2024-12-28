using Application.Common.Models;
using MediatR;
using Domain.Dto.Appointments;
using Domain.Dto.Common;

namespace Application.Appointments.Queries
{
    public class AppointmentListQuery : IRequest<ResponseDetail<PaginatedResult<AppointmentReadDto>>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}

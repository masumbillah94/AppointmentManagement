using Application.Appointments.Commands;
using AutoMapper;
using Domain.Dto.Appointments;
using Domain.Dto.Common;
using Domain.Entities.Appointments;

namespace Application.Appointments.Mappers
{
    public class AppointmentProfile : Profile
    {
        #region Public Constructors

        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentAddCommand>().ReverseMap();
            CreateMap<Appointment, PaginatedResult<AppointmentReadDto>>().ReverseMap();
            CreateMap<Appointment, AppointmentReadDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDeleteCommand>().ReverseMap();
            CreateMap<Appointment, AppointmentUpdateCommand>().ReverseMap();
        }

        #endregion Public Constructors
    }
}

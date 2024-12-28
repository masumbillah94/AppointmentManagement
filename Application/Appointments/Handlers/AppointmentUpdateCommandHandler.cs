using Application.Common.BaseHandler;
using Application.Common.Models;
using Application.Appointments.Commands;
using AutoMapper;
using Domain.Abstractions.Base;
using Domain.Dto.Appointments;
using Domain.Entities.Appointments;
using MediatR;

namespace Application.Appointments.Handlers
{
    public class AppointmentUpdateCommandHandler : BaseHandler, IRequestHandler<AppointmentUpdateCommand, ResponseDetail<AppointmentReadDto>>
    {
        #region Public Constructors

        public AppointmentUpdateCommandHandler(IRepositoryFacade unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ResponseDetail<AppointmentReadDto>> Handle(AppointmentUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = ApplicationFactory.CreateResponseDetails<AppointmentReadDto>();
            try
            {
                var appointment = _mapper.Map<Appointment>(request);
                await _repositoryFacade.AppointmentRepo.UpdateEntityAsync(appointment);
                var result = await _repositoryFacade.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    var appointmentDto = _mapper.Map<AppointmentReadDto>(appointment);
                    response.SuccessResponse(appointmentDto, "Appointment updated successfully");
                }
                else
                {
                    response.InvalidResponse("Appointment Not Created");
                }
                return response;
            }
            catch (Exception ex)
            {
                return response.ErrorResponse(ex);
            }
        }

        #endregion Public Methods
    }
}

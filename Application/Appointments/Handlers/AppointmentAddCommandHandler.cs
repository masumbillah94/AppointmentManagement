using Application.Common.BaseHandler;
using Application.Common.Models;
using Application.Appointments.Commands;
using AutoMapper;
using Domain.Abstractions.Base;
using MediatR;
using Domain.Dto.Appointments;
using Domain.Entities.Appointments;

namespace Application.Appointments.Handlers
{
    public class AppointmentAddCommandHandler : BaseHandler, IRequestHandler<AppointmentAddCommand, ResponseDetail<AppointmentReadDto>>
    {
        #region Public Constructors

        public AppointmentAddCommandHandler(IRepositoryFacade unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ResponseDetail<AppointmentReadDto>> Handle(AppointmentAddCommand request, CancellationToken cancellationToken)
        {
            var response = ApplicationFactory.CreateResponseDetails<AppointmentReadDto>();
            try
            {
                var appointment = _mapper.Map<Appointment>(request);
                await _repositoryFacade.AppointmentRepo.AddEntityAsync(appointment);
                var result = await _repositoryFacade.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    var appointmentDto = _mapper.Map<AppointmentReadDto>(appointment);
                    response.SuccessResponse(appointmentDto, "Appointment added successfully");
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

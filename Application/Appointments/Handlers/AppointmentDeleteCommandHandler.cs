using Application.Common.BaseHandler;
using Application.Common.Models;
using Application.Appointments.Commands;
using AutoMapper;
using Domain.Abstractions.Base;
using Domain.Dto.Appointments;
using MediatR;

namespace Application.Appointments.Handlers
{
    public class AppointmentDeleteCommandHandler : BaseHandler, IRequestHandler<AppointmentDeleteCommand, ResponseDetail<AppointmentReadDto>>
    {
        #region Public Constructors

        public AppointmentDeleteCommandHandler(IRepositoryFacade unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ResponseDetail<AppointmentReadDto>> Handle(AppointmentDeleteCommand request, CancellationToken cancellationToken)
        {
            var response = ApplicationFactory.CreateResponseDetails<AppointmentReadDto>();
            try
            {
                var appointment = await _repositoryFacade.AppointmentRepo.GetByIdAsync(request.Id);
                if (appointment == null)
                {
                    return response.InvalidResponse("Appointment Not found");
                }
                await _repositoryFacade.AppointmentRepo.DeleteByIdAsync(request.Id);
                var result = await _repositoryFacade.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    var appointmentDto = _mapper.Map<AppointmentReadDto>(appointment);
                    response.SuccessResponse(appointmentDto, "Appointment deleted successfully.");
                }
                else
                {
                    response.InvalidResponse("Appointment not deleted.");
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

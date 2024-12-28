using Application.Common.BaseHandler;
using Application.Common.Models;
using Application.Appointments.Queries;
using AutoMapper;
using Domain.Abstractions.Base;
using Domain.Dto.Appointments;
using MediatR;

namespace Application.Appointments.Handlers
{
    public class AppointmentByIdQueryHandler : BaseHandler, IRequestHandler<AppointmentByIdQuery, ResponseDetail<AppointmentReadDto>>
    {
        #region Public Constructors

        public AppointmentByIdQueryHandler(IRepositoryFacade unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ResponseDetail<AppointmentReadDto>> Handle(AppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = ApplicationFactory.CreateResponseDetails<AppointmentReadDto>();
            try
            {
                var appointment = await _repositoryFacade.AppointmentRepo.GetByIdAsync(request.Id);

               
                response.SuccessResponse(appointment);
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

using Application.Common.BaseHandler;
using Application.Common.Models;
using Application.Appointments.Queries;
using AutoMapper;
using Domain.Abstractions.Base;
using Domain.Dto.Appointments;
using MediatR;
using Domain.Dto.Common;

namespace Application.Appointments.Handlers
{
    public class AppointmentListQueryHandler : BaseHandler, IRequestHandler<AppointmentListQuery, ResponseDetail<PaginatedResult<AppointmentReadDto>>>
    {
        #region Public Constructors

        public AppointmentListQueryHandler(IRepositoryFacade unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ResponseDetail<PaginatedResult<AppointmentReadDto>>> Handle(AppointmentListQuery request, CancellationToken cancellationToken)
        {
            var response = ApplicationFactory.CreatePaginationResponseDetails<AppointmentReadDto>();
            try
            {
                var appointmentList = await _repositoryFacade.AppointmentRepo.GetAllAsync( request.pageNumber,request.pageSize);
                
                response.SuccessResponse(appointmentList);
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

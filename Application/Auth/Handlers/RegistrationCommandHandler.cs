using Application.Common.BaseHandler;
using Application.Common.Models;
using Application.Appointments.Commands;
using AutoMapper;
using Domain.Abstractions.Base;
using MediatR;
using Domain.Dto.Appointments;
using Domain.Entities.Appointments;
using Domain.Dto.Users;
using Application.Auth.Commands;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Handlers
{
    public class RegistrationCommandHandler : BaseHandler, IRequestHandler<RegisrtrationCommand, ResponseDetail<UserReadDto>>
    {
        private readonly IPasswordHasher _passwordHasher;
        #region Public Constructors

        public RegistrationCommandHandler(IRepositoryFacade unitOfWork, IMapper mapper, IPasswordHasher passwordHasher) : base(unitOfWork, mapper)
        {
            _passwordHasher = passwordHasher;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ResponseDetail<UserReadDto>> Handle(RegisrtrationCommand request, CancellationToken cancellationToken)
        {
            var response = ApplicationFactory.CreateResponseDetails<UserReadDto>();
            try
            {
                var user = _mapper.Map<User>(request);
                user.Password = _passwordHasher.HashPassword(user.Username, user.Password);
                await _repositoryFacade.UserRepo.AddUserAsync(user);
                var result = await _repositoryFacade.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    var userDto = _mapper.Map<UserReadDto>(user);
                    userDto.Password = null!;
                    response.SuccessResponse(userDto, "User added successfully");
                }
                else
                {
                    response.InvalidResponse("User Not Created");
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

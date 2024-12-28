using Application.Common.BaseHandler;
using Application.Common.Models;
using AutoMapper;
using Domain.Abstractions.Base;
using MediatR;
using Domain.Dto.Users;
using Application.Auth.Commands;
using Domain.Entities.Users;
using Domain.Dto.Common;
using Application.Services;
using Domain.Utils;

namespace Application.Auth.Handlers
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommand, ResponseDetail<UserTokens>>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtSettings _jwtSettings;
        #region Public Constructors

        public LoginCommandHandler(IRepositoryFacade unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, JwtSettings jwtSettings) : base(unitOfWork, mapper)
        {
            _passwordHasher = passwordHasher;
            _jwtSettings = jwtSettings;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ResponseDetail<UserTokens>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = ApplicationFactory.CreateResponseDetails<UserTokens>();
            try
            {
                var user = await _repositoryFacade.UserRepo.GetUserByUserName(request.Username);
                var matchPassword = _passwordHasher.HashPassword(request.Username, request.Password);
                if (user != null && user.Password.Equals(matchPassword, StringComparison.Ordinal)) 
                {
                    var usertoken = _mapper.Map<UserTokens>(user);
                    usertoken = JwtHelper.GenTokenkey(usertoken, _jwtSettings);
                    return response.SuccessResponse(usertoken);
                }
                return response.InvalidResponse("Authentication failed");
            }
            catch (Exception ex)
            {
                return response.ErrorResponse(ex);
            }
        }



        #endregion Public Methods
    }
}

using Application.Common.Models;
using MediatR;
using Domain.Dto.Users;
using Domain.Dto.Common;

namespace Application.Auth.Commands
{
    public class LoginCommand : IRequest<ResponseDetail<UserTokens>>
    {
        #region Public Properties
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #endregion Public Properties
    }
}

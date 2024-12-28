using Application.Common.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Doctors;
using Domain.Dto.Users;

namespace Application.Auth.Commands
{
    public class RegisrtrationCommand : IRequest<ResponseDetail<UserReadDto>>
    {
        #region Public Properties
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #endregion Public Properties
    }
}

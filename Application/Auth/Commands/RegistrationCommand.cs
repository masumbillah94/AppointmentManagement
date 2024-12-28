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
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        #endregion Public Properties
    }
}

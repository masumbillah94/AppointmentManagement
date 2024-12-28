using Application.Auth.Commands;
using AutoMapper;
using Domain.Dto.Common;
using Domain.Dto.Users;
using Domain.Entities.Users;

namespace Application.Appointments.Mappers
{
    public class AuthtProfile : Profile
    {
        #region Public Constructors

        public AuthtProfile()
        {
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, UserTokens>().ReverseMap();
            CreateMap<User, RegisrtrationCommand>().ReverseMap();
        }

        #endregion Public Constructors
    }
}

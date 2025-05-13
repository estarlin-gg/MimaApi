using AutoMapper;
using Mima.Application.Dtos;
using Mima.Domain.Model;

namespace Mima.Application.Mappers
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<LoginDto, User>();
            
        }
    }
}

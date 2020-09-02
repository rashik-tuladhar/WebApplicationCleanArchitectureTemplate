using Application.DTOs.Authentication.UserDTOs;
using AutoMapper;

namespace Application.MappingConfigurations.Authentication
{
    public class UserMappers : Profile
    {
        public UserMappers()
        {
            CreateMap<UserViewModel, UserParams>();
        }
    }
}

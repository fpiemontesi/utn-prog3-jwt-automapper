using AutoMapper;
using UserApi.Context;
using UserApi.Dtos;

namespace UserApi
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<User, UserDto>();
        }
    }
}

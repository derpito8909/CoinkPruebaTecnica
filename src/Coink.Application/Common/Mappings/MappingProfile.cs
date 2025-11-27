using AutoMapper;
using Coink.Application.Locations.Dtos;
using Coink.Application.Users.Dtos;
using Coink.Domain.Entities;

namespace Coink.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<Country, CountryDto>();

        CreateMap<Department, DepartmentDto>();

        CreateMap<Municipality, MunicipalityDto>();
    }
}

using AutoMapper;
using DeveloperApi.Dtos;
using DeveloperApi.Entities;

namespace DeveloperApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Developer, DeveloperDto>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.ToString()));

        CreateMap<CreateDeveloperDto, Developer>();

        CreateMap<UpdateDeveloperDto, Developer>();
    }
}

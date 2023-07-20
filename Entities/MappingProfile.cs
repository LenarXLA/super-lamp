using AutoMapper;
using Project.WebApi.Entities.DataTransferObjects;
using Project.WebApi.Entities.Models;

namespace Project.WebApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegistrationDto, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
    }
}

using AutoMapper;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;

namespace SocialNetwork.Infrastructure.Mappings;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserInformationDto, User>().ReverseMap();
        CreateMap<RegistrationDto, User>().ReverseMap();
    }
}

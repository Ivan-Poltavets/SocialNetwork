using AutoMapper;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;

namespace SocialNetwork.Infrastructure.Mappings;

public class UserInformationDtoProfile : Profile
{
	public UserInformationDtoProfile()
	{
		CreateMap<User, UserInformationDto>();
	}
}

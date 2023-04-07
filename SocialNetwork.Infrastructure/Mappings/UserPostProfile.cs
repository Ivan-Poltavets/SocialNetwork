using AutoMapper;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;

namespace SocialNetwork.Infrastructure.Mappings;

public class UserPostProfile : Profile
{
	public UserPostProfile()
	{
		CreateMap<UserPostDto, UserPost>().ReverseMap();
	}
}

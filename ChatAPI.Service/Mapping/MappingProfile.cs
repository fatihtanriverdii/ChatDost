using AutoMapper;
using ChatAPI.Core.DTOs;
using ChatAPI.Core.Models;

namespace ChatAPI.Service.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile() {
			CreateMap<RegisterDto, User>()
				.ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
				.ForMember(dest => dest.Role, opt => opt.Ignore());

			CreateMap<ChatRoom, ChatRoomDto>()
				.ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count))
				.ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.Messages.Any()
														? src.Messages.OrderByDescending(m => m.SentAt).First().Content
														: string.Empty
							)
				);

			CreateMap<User, UserProfileDto>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

			CreateMap<UpdateUserDto, User>()
				.ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
				.ForMember(dest => dest.Role, opt => opt.Ignore())
				.ForMember(dest => dest.Email, opt => opt.Ignore());
		}
	}
}

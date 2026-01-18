using AutoMapper;
using ProjectManager.Domain.Users;

namespace ProjectManager.Application.Users.Dtos;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>()
            .ForCtorParam(nameof(User.Email), 
                opt => opt.MapFrom(src => src.Email.Value));
    }
}
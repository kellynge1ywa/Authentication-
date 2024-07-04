using AutoMapper;

namespace authentication;

public class AppProfiles:Profile
{
    public AppProfiles()
    {
        CreateMap<RegisterUserDto, User>().ReverseMap();
    }
}

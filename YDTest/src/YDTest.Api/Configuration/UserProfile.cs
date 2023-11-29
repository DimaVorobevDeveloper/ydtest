using AutoMapper;
using YDTest.Data.Entities;
using YDTest.Model;
using YDTest.Model.Api;

namespace YDTest.Api.Configuration;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, User>(MemberList.Source);
        CreateMap<User, UserDto>(MemberList.Source)
            .ForMember(x => x.Age, opt => opt.MapFrom(x => GetAge(x.Birth)));
    }

    private int GetAge(DateTime birth)
    {
        // Save today's date.
        var today = DateTime.Today;

        // Calculate the age.
        var age = today.Year - birth.Year;

        // Go back to the year in which the person was born in case of a leap year
        if (birth.Date > today.AddYears(-age)) age--;

        return age;
    }
}

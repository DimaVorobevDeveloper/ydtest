using AutoMapper;
using YDTest.Data.Entities;
using YDTest.Model.Api;
using YDTest.Model.Dto;

namespace YDTest.Api.Configuration;

public class UserTeamProfile : Profile
{
    public UserTeamProfile()
    {
        CreateMap<CreateUserTeamRequest, UserTeam>(MemberList.Source);
        CreateMap<UserTeam, UserTeamDto>(MemberList.Source);
    }
}

using AutoMapper;
using YDTest.Data.Entities;
using YDTest.Model.Api;
using YDTest.Model;

namespace YDTest.Api.Configuration;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<CreateTeamRequest, Team>(MemberList.Source);
        CreateMap<Team, TeamDto>(MemberList.Source);
    }
}

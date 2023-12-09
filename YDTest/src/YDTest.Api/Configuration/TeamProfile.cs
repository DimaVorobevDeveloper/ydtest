using AutoMapper;
using YDTest.Data.Entities;
using YDTest.Model.Api;
using YDTest.Model.Dto;

namespace YDTest.Api.Configuration;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<CreateTeamRequest, Team>(MemberList.Source);
        CreateMap<UpdateTeamRequest, Team>(MemberList.Source);
        CreateMap<Team, TeamDto>(MemberList.Source)
            .ForMember(x => x.Users, opt => opt.MapFrom(x => x.Users))
            .ForMember(x => x.UserTeams, opt => opt.MapFrom(x => x.UserTeams));
    }
}

using YDTest.Model;
using YDTest.Model.Api;

namespace YDTest.Logic.Abstractions;

public interface ITeamLogic
{
    List<TeamDto> GetTeams();

    Task<TeamDto> GetTeam(string id);

    Task<TeamDto> CreateTeam(CreateTeamRequest request);

    Task<TeamDto> UpdateTeam(string id, UpdateTeamRequest request);
}

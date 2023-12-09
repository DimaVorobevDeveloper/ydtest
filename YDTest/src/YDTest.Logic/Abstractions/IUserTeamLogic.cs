using YDTest.Model.Api;
using YDTest.Model.Dto;

namespace YDTest.Logic.Abstractions;

public interface IUserTeamLogic
{
    List<UserTeamDto> GetUserTeams();

    Task<UserTeamDto> GetUserTeam(string id);

    Task<UserTeamDto> GetUserTeam2(string id);

    Task<UserTeamDto> CreateUserTeam(CreateUserTeamRequest request);

    Task<UserTeamDto> DeleteUserTeam(string id);
}

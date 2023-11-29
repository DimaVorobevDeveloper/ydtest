using YDTest.Model.Abstractions;
using YDTest.Model.Enums;

namespace YDTest.Model;

public class TeamDto : EntityBaseDto
{
    public string Name { get; set; }

    public TeamType Type { get; set; }

    public DateTime Expired { get; set; }

    public List<UserTeamDto> UserTeams { get; } = new();

    public List<UserDto> Users { get; } = new();
}

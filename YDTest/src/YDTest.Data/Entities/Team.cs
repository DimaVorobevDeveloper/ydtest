using YDTest.Data.Abstractions;
using YDTest.Model.Enums;

namespace YDTest.Data.Entities;

public class Team : EntityBase
{
    public string Name { get; set; }

    public TeamType Type { get; set; }

    public DateTime Expired { get; set; }

    public List<UserTeam> UserTeams { get; } = new();

    public List<User> Users { get; } = new();
}

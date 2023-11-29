using YDTest.Data.Abstractions;
using YDTest.Model.Enums;

namespace YDTest.Data.Entities;

public class Team : EntityBase
{
    public string Token { get; set; }

    public TokenType Type { get; set; }

    public DateTime Expired { get; set; }

    public List<UserTeam> UserTeams { get; } = new();
    public List<User> Users { get; } = new();
}

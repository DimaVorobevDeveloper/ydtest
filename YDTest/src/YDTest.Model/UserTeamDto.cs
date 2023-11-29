namespace YDTest.Model;

public class UserTeamDto
{
    public Guid UserId { get; set; }

    public Guid TeamId { get; set; }

    public UserDto User { get; set; } = null!;

    public TeamDto Team { get; set; } = null!;
}


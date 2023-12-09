using YDTest.Model.Abstractions;

namespace YDTest.Model.Dto;

public class UserTeamDto : EntityBaseDto
{
    public Guid UserId { get; set; }

    public Guid TeamId { get; set; }

    public UserDto User { get; set; } = null!;

    public TeamDto Team { get; set; } = null!;
}


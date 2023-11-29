using System.ComponentModel.DataAnnotations;
using YDTest.Model;
using YDTest.Model.Abstractions;
using YDTest.Model.Enums;

namespace YDTest.Model.Api;

public class CreateTeamRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public TeamType Type { get; set; }

    [Required]
    public DateTime Expired { get; set; }

    public List<UserTeamDto> UserTeams { get; } = new();

    public List<UserDto> Users { get; } = new();
}

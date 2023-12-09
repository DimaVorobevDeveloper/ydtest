using System.ComponentModel.DataAnnotations;
using YDTest.Model.Abstractions;
using YDTest.Model.Enums;

namespace YDTest.Model.Api;

public class UpdateTeamRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public TeamType Type { get; set; }

    [Required]
    public DateTime Expired { get; set; }
}


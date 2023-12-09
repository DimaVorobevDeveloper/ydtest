using YDTest.Model.Abstractions;

namespace YDTest.Model.Dto;

public class UserDto : EntityBaseDto
{
    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime Birth { get; set; }

    public int Age { get; set; }

    public string City { get; set; }

    public List<TeamDto> Teams { get; } = new();
}

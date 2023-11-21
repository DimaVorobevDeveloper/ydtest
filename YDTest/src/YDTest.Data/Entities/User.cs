using YDTest.Data.Abstractions;

namespace YDTest.Data.Entities;

public class User : EntityBase
{
    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime Birth { get; set; }

    public string City { get; set; }
}

using System.ComponentModel.DataAnnotations;
using YDTest.Model.Abstractions;

namespace YDTest.Model.Api;

public class CreateUserRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime Birth { get; set; }

    [Required]
    public string City { get; set; }
}

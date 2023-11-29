using System.ComponentModel.DataAnnotations;

namespace YDTest.Common.Models;

public class UpdateUserRequest
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
namespace YDTest.Data.Entities;

public class User
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime Birth { get; set; }

    public int Age => GetAge();

    public string City { get; set; }

    private int GetAge()
    {
        // Save today's date.
        var today = DateTime.Today;

        // Calculate the age.
        var age = today.Year - Birth.Year;

        // Go back to the year in which the person was born in case of a leap year
        if (Birth.Date > today.AddYears(-age)) age--;

        return age;
    }
}

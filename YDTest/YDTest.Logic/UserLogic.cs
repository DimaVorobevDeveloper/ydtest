﻿using YDTest.Logic.Abstractions;
using YDTest.Model;

namespace YDTest.Logic;

public class UserLogic : IUserLogic
{
    private static readonly string[] Names = new[]
    {
        "Василий", "Василий", "Иван", "Виктор", "Василий", "Виктор", "Balmy", "Владислав", "Виктор", "Сергей"
    };

    private static readonly string[] Emails = new[]
    {
        "video.1kito@gmail.com", "photo.1kito@gmail.com", "ramil.1kito@gmail.com", "sushi.1kito@gmail.com", "video.1kito@gmail.com"
    };

    private static readonly string[] Cities = new[]
    {
        "Зеленодольск", "Казань", "Саратов", "Набережные челны", "Саратов", "Саратов", "Саратов", "Саратов", "Саратов", "Саратов"
    };

    public List<User> GetUsers()
    {
        return Enumerable.Range(1, 3).Select(index => new User
        {
            Name = Names[Random.Shared.Next(Names.Length)],
            Birth = DateTime.Now.AddYears(-Random.Shared.Next(-20, 55)),
            Email = Emails[Random.Shared.Next(Emails.Length)],
            City = Cities[Random.Shared.Next(Cities.Length)],
        }).ToList();
    }
}

using YDTest.Data;
using YDTest.Logic.Abstractions;
using YDTest.Model;

namespace YDTest.Logic;

public class UserLogic : IUserLogic
{
    private readonly YDTestContext _ydTestContext;

    public UserLogic(YDTestContext ydTestContext)
    {
        _ydTestContext = ydTestContext;
    }

    public List<UserDto> GetUsers()
    {
        var users = new List<UserDto>();

        var usersDb = _ydTestContext.Users;
        var count = usersDb.Count();

        return usersDb.Select(u => new UserDto
        {
            Name = u.Name,
            Birth = u.Birth,
            Email = u.Email,
            City = u.City,
        }).ToList();
    }

    //public List<UserDto> GetUsers()
    //{
    //    var users = new List<UserDto>();

    //    var usersDb = _ydTestContext.Users;
    //    var count = usersDb.Count();

    //    return Enumerable.Range(1, 3).Select(index => new UserDto
    //    {
    //        Name = Names[Random.Shared.Next(Names.Length)],
    //        Birth = DateTime.Now.AddYears(-Random.Shared.Next(10, 55)),
    //        Email = Emails[Random.Shared.Next(Emails.Length)],
    //        City = Cities[Random.Shared.Next(Cities.Length)],
    //    }).ToList();
    //}
}

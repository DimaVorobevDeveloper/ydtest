using YDTest.Model;

namespace YDTest.Logic.Abstractions;

public interface IUserLogic
{
    List<UserDto> GetUsers();
}

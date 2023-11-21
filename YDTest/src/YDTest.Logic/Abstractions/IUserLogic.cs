using YDTest.Common.Models;
using YDTest.Model;

namespace YDTest.Logic.Abstractions;

public interface IUserLogic
{
    List<UserDto> GetUsers();

    Task<UserDto> GetUser(string id);

    Task<UserDto> CreateUser(CreateUserRequest request);
}

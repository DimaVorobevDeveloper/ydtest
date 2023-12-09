using YDTest.Model.Api;
using YDTest.Model.Dto;

namespace YDTest.Logic.Abstractions;

public interface IUserLogic
{
    List<UserDto> GetUsers();

    Task<UserDto> GetUser(string id);

    Task<UserDto> CreateUser(CreateUserRequest request);

    Task<UserDto> UpdateUser(string id, UpdateUserRequest request);
}

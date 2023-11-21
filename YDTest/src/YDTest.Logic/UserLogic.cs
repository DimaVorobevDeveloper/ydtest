using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using YDTest.Common.Models;
using YDTest.Data;
using YDTest.Data.Entities;
using YDTest.Logic.Abstractions;
using YDTest.Model;

namespace YDTest.Logic;

public class UserLogic : IUserLogic
{
    private readonly YDTestContext _ydTestContext;
    private readonly ILogger<UserLogic> _logger;
    private readonly IMapper _mapper;

    public UserLogic(YDTestContext ydTestContext, ILogger<UserLogic> logger, IMapper mapper)
    {
        _ydTestContext = ydTestContext;
        _logger = logger;
        _mapper = mapper;
    }

    public List<UserDto> GetUsers()
    {
        var usersDb = _ydTestContext.Users;
        var usersDto = _mapper.Map<List<UserDto>>(usersDb);
        return usersDto;
    }

    public async Task<UserDto> GetUser(string id)
    {
        var userDb = await _ydTestContext.Users.SingleOrDefaultAsync(x => x.Id == new Guid(id));
        var userDto = _mapper.Map<UserDto>(userDb);

        return userDto;
    }

    public async Task<UserDto> CreateUser(CreateUserRequest request)
    {
        var user = _mapper.Map<User>(request);
        user.Created = DateTime.Now;

        var usersDb = await _ydTestContext.AddAsync(user);
        var saved = await _ydTestContext.SaveChangesAsync();

        if (saved == 0)
        {
            throw new Exception("Save error");
        }

        var userDto = _mapper.Map<UserDto>(usersDb.Entity);

        return userDto;
    }
}

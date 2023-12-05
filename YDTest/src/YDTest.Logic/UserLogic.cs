using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using YDTest.Data;
using YDTest.Data.Entities;
using YDTest.Logic.Abstractions;
using YDTest.Model.Api;
using YDTest.Model.Dto;

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

        var userDb = await _ydTestContext.AddAsync(user);
        var userDto = await Save(userDb.Entity);
        
        return userDto;
    }

    public async Task<UserDto> UpdateUser(string id, UpdateUserRequest request)
    {
        await CheckIfUserExisted(id);
        
        var userDb = _mapper.Map<User>(request);
        userDb.Id = new Guid(id);
        userDb.Modified = DateTime.Now;

        var userDto = await Save(userDb);
        return userDto;
    }

    private async Task<UserDto> Save(User userDb)
    {
        var saved = await _ydTestContext.SaveChangesAsync();

        if (saved == 0)
        {
            throw new Exception("Save error");
        }

        var userDto = _mapper.Map<UserDto>(userDb);

        return userDto;
    }

    private async Task CheckIfUserExisted(string id)
    {
        var userDb = await _ydTestContext.Users.SingleOrDefaultAsync(x=> x.Id == new Guid(id));

        if (userDb == null)
        {
            throw new Exception($"User with id {id} not found");
        }
    }
}

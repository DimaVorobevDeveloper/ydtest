using Microsoft.AspNetCore.Mvc;
using YDTest.Common.Models;
using YDTest.Logic.Abstractions;
using YDTest.Model;

namespace YDTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserLogic userLogic, ILogger<UsersController> logger)
    {
        _userLogic = userLogic;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<UserDto> Get()
    {
        try
        {
            _logger.LogInformation("Get users");
            return _userLogic.GetUsers();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get users error: " + ex.Message);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<UserDto> Get(string id)
    {
        _logger.LogInformation("Get users");
        return await _userLogic.GetUser(id);
    }

    [HttpPost]
    public async Task<UserDto> Create(CreateUserRequest model)
    {
        _logger.LogInformation("Create user");
        return await _userLogic.CreateUser(model);
    }
    
    [HttpPut("{id}")]
    public async Task<UserDto> Update(string id, UpdateUserRequest model)
    {
        _logger.LogInformation("Update user");
        return await _userLogic.UpdateUser(id, model);
    }
}

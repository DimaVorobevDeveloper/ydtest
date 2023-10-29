using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<User> Get()
    {
        _logger.LogInformation("Get users");
        return _userLogic.GetUsers();
    }
}

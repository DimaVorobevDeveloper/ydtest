using Microsoft.AspNetCore.Mvc;
using YDTest.Logic.Abstractions;
using YDTest.Model.Api;
using YDTest.Model.Dto;

namespace YDTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTeamController : ControllerBase
{
    private readonly IUserTeamLogic _userTeamLogic;
    private readonly UnitOfWork _unitOfWork;
    private readonly ILogger<UserTeamController> _logger;

    public UserTeamController(IUserTeamLogic userTeamLogic, UnitOfWork unitOfWork, ILogger<UserTeamController> logger)
    {
        _userTeamLogic = userTeamLogic;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<UserTeamDto> Get()
    {
        try
        {
            _logger.LogInformation("Get user team");
            return _userTeamLogic.GetUserTeams();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get user team error: " + ex.Message);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<UserTeamDto> Get(string id)
    {
        _logger.LogInformation("Get user teams");
        
        var t = await _userTeamLogic.GetUserTeam2(id);
        return await _userTeamLogic.GetUserTeam(id);
    }

    [HttpPost]
    public async Task<UserTeamDto> Create(CreateUserTeamRequest model)
    {
        _logger.LogInformation("Create user team");
        return await _userTeamLogic.CreateUserTeam(model);
    }

    [HttpDelete("{id}")]
    public async Task<UserTeamDto> Update(string id)
    {
        _logger.LogInformation("Delete user team");
        return await _userTeamLogic.DeleteUserTeam(id);
    }
}

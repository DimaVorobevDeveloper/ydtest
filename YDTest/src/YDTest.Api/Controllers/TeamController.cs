using Microsoft.AspNetCore.Mvc;
using YDTest.Logic.Abstractions;
using YDTest.Model;
using YDTest.Model.Api;

namespace YDTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeamLogic _teamLogic;
    private readonly ILogger<TeamController> _logger;

    public TeamController(ITeamLogic teamLogic, ILogger<TeamController> logger)
    {
        _teamLogic = teamLogic;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<TeamDto> Get()
    {
        try
        {
            _logger.LogInformation("Get teams");
            return _teamLogic.GetTeams();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get teams error: " + ex.Message);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<TeamDto> Get(string id)
    {
        _logger.LogInformation("Get teams");
        return await _teamLogic.GetTeam(id);
    }

    [HttpPost]
    public async Task<TeamDto> Create(CreateTeamRequest model)
    {
        _logger.LogInformation("Create team");
        return await _teamLogic.CreateTeam(model);
    }

    [HttpPut("{id}")]
    public async Task<TeamDto> Update(string id, UpdateTeamRequest model)
    {
        _logger.LogInformation("Update team");
        return await _teamLogic.UpdateTeam(id, model);
    }
}


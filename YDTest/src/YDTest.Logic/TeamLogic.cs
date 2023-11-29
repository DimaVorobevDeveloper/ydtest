using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YDTest.Data.Entities;
using YDTest.Data;
using YDTest.Logic.Abstractions;
using YDTest.Model;
using YDTest.Model.Api;

namespace YDTest.Logic;

public class TeamLogic : ITeamLogic
{
    private readonly YDTestContext _ydTestContext;
    private readonly ILogger<TeamLogic> _logger;
    private readonly IMapper _mapper;

    public TeamLogic(YDTestContext ydTestContext, ILogger<TeamLogic> logger, IMapper mapper)
    {
        _ydTestContext = ydTestContext;
        _logger = logger;
        _mapper = mapper;
    }

    public List<TeamDto> GetTeams()
    {
        var teamsDb = _ydTestContext.Teams;
        var teamsDto = _mapper.Map<List<TeamDto>>(teamsDb);
        return teamsDto;
    }

    public async Task<TeamDto> GetTeam(string id)
    {
        var teamDb = await _ydTestContext.Teams.SingleOrDefaultAsync(x => x.Id == new Guid(id));
        var teamDto = _mapper.Map<TeamDto>(teamDb);

        return teamDto;
    }

    public async Task<TeamDto> CreateTeam(CreateTeamRequest request)
    {
        var team = _mapper.Map<Team>(request);
        team.Created = DateTime.Now;

        var teamDb = await _ydTestContext.AddAsync(team);
        var teamDto = await Save(teamDb.Entity);

        return teamDto;
    }

    public async Task<TeamDto> UpdateTeam(string id, UpdateTeamRequest request)
    {
        await CheckIfTeamExisted(id);

        var teamDb = _mapper.Map<Team>(request);
        teamDb.Id = new Guid(id);
        teamDb.Modified = DateTime.Now;

        var teamDto = await Save(teamDb);
        return teamDto;
    }

    private async Task<TeamDto> Save(Team teamDb)
    {
        var saved = await _ydTestContext.SaveChangesAsync();

        if (saved == 0)
        {
            throw new Exception("Save error");
        }

        var teamDto = _mapper.Map<TeamDto>(teamDb);

        return teamDto;
    }

    private async Task CheckIfTeamExisted(string id)
    {
        var teamDb = await _ydTestContext.Teams.SingleOrDefaultAsync(x => x.Id == new Guid(id));

        if (teamDb != null)
        {
            throw new Exception($"Team with id {id} not found");
        }
    }
}


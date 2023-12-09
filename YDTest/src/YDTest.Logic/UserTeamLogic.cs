using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YDTest.Data.Entities;
using YDTest.Data;
using YDTest.Logic.Abstractions;
using YDTest.Model.Api;
using YDTest.Model.Dto;

namespace YDTest.Logic;

public class UserTeamLogic : IUserTeamLogic
{
    private readonly YDTestContext _ydTestContext;
    private readonly UnitOfWork _unitOfWork;

    private readonly ILogger<UserTeamLogic> _logger;
    private readonly IMapper _mapper;

    public UserTeamLogic(YDTestContext ydTestContext, ILogger<UserTeamLogic> logger, IMapper mapper, UnitOfWork unitOfWork)
    {
        _ydTestContext = ydTestContext;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public List<UserTeamDto> GetUserTeams()
    {
        var userTeamsDb = _ydTestContext.UserTeams.Where(x => x.IsDeleted != true);
        var userTeamsDto = _mapper.Map<List<UserTeamDto>>(userTeamsDb);
        return userTeamsDto;
    }

    public async Task<UserTeamDto> GetUserTeam(string id)
    {
        var userTeamDb = await _ydTestContext.UserTeams
            .SingleOrDefaultAsync(x => x.Id == new Guid(id));

        var userTeamDto = _mapper.Map<UserTeamDto>(userTeamDb);

        return userTeamDto;
    }

    public async Task<UserTeamDto> GetUserTeam2(string id)
    {
        var userTeamDb = await _unitOfWork.UserRepository.GetById(id);
        _unitOfWork.Save();

        var userTeamDto = _mapper.Map<UserTeamDto>(userTeamDb);

        return userTeamDto;
    }

    public async Task<UserTeamDto> CreateUserTeam(CreateUserTeamRequest request)
    {
        var userTeam = _mapper.Map<UserTeam>(request);
        userTeam.Created = DateTime.Now;

        var userTeamDb = await _ydTestContext.AddAsync(userTeam);
        var userTeamDto = await Save(userTeamDb.Entity);

        return userTeamDto;
    }

    public async Task<UserTeamDto> DeleteUserTeam(string id)
    {
        var userTeam = await CheckIfUserTeamExisted(id);

        var userTeamDb = _ydTestContext.Remove(userTeam);

        var userTeamDto = _mapper.Map<UserTeamDto>(userTeamDb.Entity);
        return userTeamDto;
    }

    private async Task<UserTeamDto> Save(UserTeam userTeamDb)
    {
        var saved = await _ydTestContext.SaveChangesAsync();

        if (saved == 0)
        {
            _logger.LogError("Save error with UserTeam");
            throw new Exception("Save error");
        }

        var userTeamDto = _mapper.Map<UserTeamDto>(userTeamDb);
        return userTeamDto;
    }

    private async Task<UserTeam> CheckIfUserTeamExisted(string id)
    {
        var userTeamDb = await _ydTestContext.UserTeams.SingleOrDefaultAsync(x => x.Id == new Guid(id));

        if (userTeamDb == null)
        {
            throw new Exception($"Team with id {id} not found");
        }

        return userTeamDb;
    }
}


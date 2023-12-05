using Microsoft.AspNetCore.Mvc;
using YDTest.Logic.Abstractions;
using YDTest.Model.Dto;

namespace YDTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly ILogsLogic _logsLogic;
    private readonly ILogger<TeamController> _logger;

    public LogsController(ILogsLogic logsLogic, ILogger<TeamController> logger)
    {
        _logsLogic = logsLogic;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<LogDto>> GetLogs()
    {
        try
        {
            _logger.LogInformation("Get logs");
            return await _logsLogic.GetLogs();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get logs error: " + ex.Message);
            throw;
        }
    }

    [HttpGet("last")]
    public async Task<string> GetLastLog()
    {
        try
        {
            _logger.LogInformation("Get last log");
            return await _logsLogic.GetLastLog();
        }
        catch (Exception ex)
        {
            _logger.LogError("Get last log error: " + ex.Message);
            throw;
        }
    }

    [HttpGet("last/{infoType}")]
    public async Task<string> GetLastLogByInfoType(string infoType = "Error")
    {
        try
        {
            _logger.LogInformation("Get last log");
            return await _logsLogic.GetLastLogByInfo(infoType);
        }
        catch (Exception ex)
        {
            _logger.LogError("Get logs error: " + ex.Message);
            throw;
        }
    }
}

using YDTest.Model.Dto;

namespace YDTest.Logic.Abstractions;

public interface ILogsLogic
{
    Task<List<LogDto>> GetLogs();

    Task<string> GetLastLog();

    Task<string> GetLastLogByInfo(string infoType);
}


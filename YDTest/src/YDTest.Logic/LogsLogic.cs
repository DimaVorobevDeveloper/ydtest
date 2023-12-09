using YDTest.Logic.Abstractions;
using YDTest.Model.Dto;

namespace YDTest.Logic;

public class LogsLogic : ILogsLogic
{
    public async Task<List<LogDto>> GetLogs()
    {
        var filePath = @"bin\Debug\net7.0\log";
        var paths = Directory.GetFiles(filePath);
        var path = paths.LastOrDefault();

        if (path == null)
            return new List<LogDto>();

        var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs);

        var logTextLines = await sr.ReadToEndAsync();
        var logsLines = logTextLines.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => new LogDto { Line = x.ToString() }).ToList();

        return logsLines;
    }

    public async Task<string> GetLastLog()
    {
        var logs = await GetLogs();
        return logs.LastOrDefault()?.Line;
    }

    public async Task<string> GetLastLogByInfo(string infoType)
    {
        var logs = await GetLogs();
        return logs.LastOrDefault(x => x.Line.Contains(infoType))?.Line;
    }
}


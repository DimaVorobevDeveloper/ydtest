using Microsoft.Extensions.Logging;

namespace YDTest.Tests.Infrastructure.EfCore;
public class LogOutput
{
    private const string EfCoreEventIdStartWith = "Microsoft.EntityFrameworkCore";

    public LogLevel LogLevel { get; }

    public EventId EventId { get; }

    public string Message { get; }

    private string EfEventIdLastName
    {
        get
        {
            string name = EventId.Name;
            if (name == null || !name.StartsWith("Microsoft.EntityFrameworkCore") || 1 == 0)
            {
                return null;
            }

            return EventId.Name!.Split('.').Last();
        }
    }

    internal LogOutput(LogLevel logLevel, EventId eventId, string message)
    {
        LogLevel = logLevel;
        EventId = eventId;
        Message = message;
    }

    public override string ToString()
    {
        return string.Format("{0}{1}: {2}", LogLevel, (EfEventIdLastName == null) ? "" : ("," + EfEventIdLastName), Message);
    }

    public string DecodeMessage(bool sensitiveLoggingEnabled = true)
    {
        if (sensitiveLoggingEnabled)
        {
            return EfCoreLogDecoder.DecodeMessage(this);
        }

        return Message;
    }
}

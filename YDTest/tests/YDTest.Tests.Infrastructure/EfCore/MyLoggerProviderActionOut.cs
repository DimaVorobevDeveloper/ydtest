using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDTest.Tests.Infrastructure.EfCore;
public class MyLoggerProviderActionOut : ILoggerProvider, IDisposable
{
    private class MyLogger : ILogger
    {
        private readonly Action<LogOutput> _efLog;

        private readonly LogLevel _logLevel;

        public MyLogger(Action<LogOutput> efLog, LogLevel logLevel)
        {
            _efLog = efLog;
            _logLevel = logLevel;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _efLog(new LogOutput(logLevel, eventId, formatter(state, exception)));
            Console.WriteLine(formatter(state, exception));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }

    private readonly Action<LogOutput> _efLog;

    private readonly LogLevel _logLevel;

    public MyLoggerProviderActionOut(Action<LogOutput> efLog, LogLevel logLevel = LogLevel.Information)
    {
        _efLog = efLog;
        _logLevel = logLevel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new MyLogger(_efLog, _logLevel);
    }

    public void Dispose()
    {
    }
}
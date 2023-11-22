using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using YDTest.Logic.UnitTests.Mocks;

namespace YDTest.Logic.UnitTests;

/// <summary>
/// Базовый класс для unit-тестов
/// </summary>
public abstract class BaseUnitTest : IDisposable
{
    protected readonly Mock<ILogger> Logger;
    protected readonly ITestOutputHelper Output;
    protected readonly System.Diagnostics.Stopwatch Stopwatch = System.Diagnostics.Stopwatch.StartNew();
    protected readonly Fixture Fixture;
    protected readonly DbContext _FakeDbContext;
    protected BaseUnitTest()
    {
        // Output = output;
        Logger = new Mock<ILogger>();
        Fixture = new Fixture();

        //var scopeWrapper = new ScopeWrapper("start", "finish", "error", new UnitTestDisposable(),
        //    Logger.Object, LogLevel.Information, null);

        Logger.Setup(x => x.IsEnabled(It.IsAny<LogLevel>())).Returns(() => true);
        //Logger.Setup(x => x.BeginScope(It.IsAny<object>())).Returns(scopeWrapper);

        //_FakeDbContext = new FakeDbContext(null);
    }

    public virtual void Dispose()
    {
        Stopwatch.Stop();
        // Output.WriteLine($"Execution time: {Stopwatch.Elapsed.TotalSeconds}");
        AssertLogErrors(Times.Never());
    }

    protected virtual void AssertLogErrors(Times times)
    {
        Logger.Verify(x => x.Log(
                It.Is<LogLevel>(level => level >= LogLevel.Warning),
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()),
            times: times);
    }

    private class UnitTestDisposable : IDisposable
    {
        public void Dispose()
        {
        }
    }

    protected virtual IConfiguration CreateConfigurationMock()
    {
        return new ConfigurationMock().Configuration;
    }
}

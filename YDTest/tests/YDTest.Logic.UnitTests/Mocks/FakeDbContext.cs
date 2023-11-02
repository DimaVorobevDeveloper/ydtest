using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using YDTest.Data;
using YDTest.Tests.Infrastructure;

namespace YDTest.Logic.UnitTests.Mocks;

public class FakeDbContext : DbContextMock<YDTestContext>
{
    public FakeDbContext(ITestOutputHelper outputHelper, LogLevel logLevel = LogLevel.Information)
        : base(logLevel, outputHelper)
    {
    }
}
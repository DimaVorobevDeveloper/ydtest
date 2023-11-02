namespace YDTest.Tests.Infrastructure.EfCore;

public static class Settings
{
    public static readonly string UnitTestConnectionStringName = "UnitTestConnection";

    public static readonly string RequiredEndingToUnitTestDatabaseName = "Test";

    private const string AppSettingFilename = "appsettings.json";

    public static string? GetEnvironment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_Environment") ??
            Environment.GetEnvironmentVariable("Hosting:Environment") ??
            Environment.GetEnvironmentVariable("ASPNET_ENV");
    }
}

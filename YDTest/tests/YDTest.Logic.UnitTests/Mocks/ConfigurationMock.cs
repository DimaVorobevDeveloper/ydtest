using Microsoft.Extensions.Configuration;

namespace YDTest.Logic.UnitTests.Mocks;

public class ConfigurationMock
{
    public readonly IConfiguration Configuration;

    public ConfigurationMock()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "LiveReporting:BanTenantId", "dp_digitalprofile1" },
            { "LiveReporting:BanTemplateName", "test_template1" },
            { "LiveReporting:BanTemplateVersionKey", "2000e2b1-7024-467f-9fd1-fddba9c2b9c5" },
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        Configuration = configuration;
    }
}

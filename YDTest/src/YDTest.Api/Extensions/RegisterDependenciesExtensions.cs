using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using System.Configuration;
using YDTest.Api.Configuration;
using YDTest.Data;
using YDTest.Logic;
using YDTest.Logic.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace YDTest.Api.Extensions;

public static class RegisterDependenciesExtensions
{
    public static void RegisterData(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        services.RegisterDb(configuration, webHostEnvironment);
        services.RegisterLogic();
        services.RegisterMapperConfiguration();
    }

    public static void RegisterDb(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        string connection = configuration.GetConnectionString("DefaultConnection");
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Info("DbInitializer connection" + connection);

        services.AddDbContext<YDTestContext>(options =>
            options.UseSqlServer(connection,
                opt => opt.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(3), errorNumbersToAdd: null)));

        if (!webHostEnvironment.IsDevelopment())
        {
            logger.Info("set managedNetworkingAppContextSwitch");
            const string managedNetworkingAppContextSwitch = "Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows";
            // AppContext.SetSwitch(managedNetworkingAppContextSwitch, true);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol |
                                                              System.Net.SecurityProtocolType.Tls11 |
                                                              System.Net.SecurityProtocolType.Tls12;
        }

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.CreateDbIfNotExists();
    }

    private static void CreateDbIfNotExists(this IServiceCollection services)
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        using var scope = serviceProvider.CreateScope();
        var services1 = scope.ServiceProvider;
        try
        {
            var context = services1.GetRequiredService<YDTestContext>();
            logger.Info("DbInitializer.Initialize(context)");
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "An error occurred creating the DB.");
        }
    }

    public static void RegisterLogic(this IServiceCollection services)
    {
        services.AddScoped<IUserLogic, UserLogic>();
    }

    public static void RegisterMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(typeof(UserProfile)));

        configuration.AssertConfigurationIsValid();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using YDTest.Data;
using YDTest.Logic;
using YDTest.Logic.Abstractions;

namespace YDTest.Api.Extensions;

public static class RegisterDependenciesExtensions
{
    public static void RegisterData(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDb(configuration);
        services.RegisterLogic();
    }

    public static void RegisterDb(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<YDTestContext>(options => options.UseSqlServer(connection));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.CreateDbIfNotExists();
    }

    private static void CreateDbIfNotExists(this IServiceCollection services)
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var services1 = scope.ServiceProvider;
            try
            {
                var context = services1.GetRequiredService<YDTestContext>();
                // var context = serviceProvider.GetRequiredService<YDTestContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services1.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }

    public static void RegisterLogic(this IServiceCollection services)
    {
        services.AddScoped<IUserLogic, UserLogic>();
    }
}

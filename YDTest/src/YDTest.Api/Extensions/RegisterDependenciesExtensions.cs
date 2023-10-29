using Microsoft.EntityFrameworkCore;
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
    }

    public static void RegisterLogic(this IServiceCollection services)
    {
        services.AddScoped<IUserLogic, UserLogic>();
    }
}

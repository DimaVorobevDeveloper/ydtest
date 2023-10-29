using YDTest.Logic;
using YDTest.Logic.Abstractions;

namespace YDTest.Api.Extensions;

public static class RegisterDependenciesExtensions
{
    public static void RegisterLogic(this IServiceCollection services)
    {
        services.AddScoped<IUserLogic, UserLogic>();
    }
}

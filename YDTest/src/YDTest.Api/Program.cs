using NLog;
using NLog.Web;
using YDTest.Api.Extensions;

var logger1 = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger1.Info("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.CaptureStartupErrors(true)
        .UseSetting("detailedErrors", "true");

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.RegisterData(builder.Configuration, builder.Environment);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    //builder.Services.AddSingleton(logger);

    //builder.Logging.Services.AddLogging();

    //builder.Logging.AddFile(o => o.RootPath = o.RootPath = builder.Environment.ContentRootPath);

    //Log.Logger = new LoggerConfiguration()
    //     .MinimumLevel.Verbose()
    //     .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "Log.txt"),
    //         rollingInterval: RollingInterval.Infinite,
    //         outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
    //     .CreateLogger();

    var app = builder.Build();

    // https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-6
    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseAuthorization();

    app.MapControllers();

    logger1.Info("before app run");

    app.Run();
}
catch (Exception exception)
{
    logger1.Info("Stopped program because of exception");
    logger1.Error(exception, exception.Message + "");
}

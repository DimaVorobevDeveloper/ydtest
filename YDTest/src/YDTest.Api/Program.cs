using Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Web;
using YDTest.Api.Extensions;

var logger1 = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
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

    builder.Services.RegisterData(builder.Configuration);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var logger = LoggerFactory.Create(config =>
    {
        config.AddConsole();
        config.AddConfiguration(builder.Configuration.GetSection("Logging"));
    }).CreateLogger("Program");


    //builder.Logging.AddFile(o => o.RootPath = o.RootPath = builder.Environment.ContentRootPath);

    //Log.Logger = new LoggerConfiguration()
    //     .MinimumLevel.Verbose()
    //     .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "Log.txt"),
    //         rollingInterval: RollingInterval.Infinite,
    //         outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
    //     .CreateLogger();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();

    app.UseAuthorization();
    logger1.Info("sssssssssssssssssssssssss Stopped program because of exception");

    app.MapControllers();

    logger1.Info("before app run");

    app.Run();
}
catch (Exception exception)
{
    logger1.Info("s2 - error");

    logger1.Error(exception, exception.Message + " Stopped program because of exception");
    // logger.LogInformation("this is debug log");
}

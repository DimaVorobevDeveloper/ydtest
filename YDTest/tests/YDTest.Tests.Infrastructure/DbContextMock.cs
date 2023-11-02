using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using YDTest.Tests.Infrastructure.EfCore;

namespace YDTest.Tests.Infrastructure;
public abstract class DbContextMock<TDbContext> : BaseMock<TDbContext, DbContextMock<TDbContext>> where TDbContext : DbContext
{
    private readonly DbConnectionType _connectionType;

    private readonly LogLevel _logLevel;

    private readonly ITestOutputHelper? _outputHelper;

    private bool _loggingStarted;

    private TDbContext? _instance;

    private readonly List<LogOutput> _logs;

    public override TDbContext Object
    {
        get
        {
            TDbContext @object = base.Object;
            _loggingStarted = true;
            return @object;
        }
    }

    private DbContextMock(string? environment, LogLevel logLevel, ITestOutputHelper? outputHelper)
    {
        _logLevel = logLevel;
        _outputHelper = outputHelper;
        _logs = new List<LogOutput>();
    }

    protected DbContextMock(LogLevel logLevel = LogLevel.Information, ITestOutputHelper? outputHelper = null)
        : this(Settings.GetEnvironment(), logLevel, outputHelper)
    {
        _connectionType = GetConnectionType();
        InitMock();
    }

    protected DbContextMock(DbConnectionType connectionType, LogLevel logLevel = LogLevel.Information, ITestOutputHelper? outputHelper = null)
        : this(Settings.GetEnvironment(), logLevel, outputHelper)
    {
        _connectionType = connectionType;
        InitMock();
    }

    protected override object OnGetObject()
    {
        if (_instance == null)
        {
            DbContextOptionsBuilder<TDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<TDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).ConfigureWarnings(delegate (WarningsConfigurationBuilder w)
            {
                w.Ignore(InMemoryEventId.TransactionIgnoredWarning);
            });
            if (_outputHelper != null)
            {
                dbContextOptionsBuilder.UseLoggerFactory(new LoggerFactory(new MyLoggerProviderActionOut[1]
                {
                    new MyLoggerProviderActionOut(AddLogAction(_outputHelper), _logLevel)
                }));
                dbContextOptionsBuilder.EnableSensitiveDataLogging();
            }

            _instance = Activator.CreateInstance(typeof(TDbContext), dbContextOptionsBuilder.Options) as TDbContext;
            if (_instance == null)
            {
                throw new Exception("Не уадлось создать объект " + typeof(TDbContext).FullName);
            }
        }

        return _instance;
    }

    private DbConnectionType GetConnectionType()
    {
        return DbConnectionType.None;
    }

    private void InitMock()
    {
        _logs.Clear();
    }

    public void SetLogsTo(ITestOutputHelper output)
    {
        ITestOutputHelper output2 = output;
        _logs.ForEach(delegate (LogOutput log)
        {
            LogToOutput(output2, log);
        });
    }

    public DbContextMock<TDbContext> UseSet<TEntity>(IEnumerable<TEntity> fakeCollection) where TEntity : class
    {
        TDbContext @object = Object;
        @object.AddRange(fakeCollection);
        @object.SaveChanges();
        return this;
    }

    public DbContextMock<TDbContext> UseSet<TEntity>(TEntity fakeEntity) where TEntity : class
    {
        return UseSet(new TEntity[1] { fakeEntity });
    }

    public DbContextMock<TDbContext> UseSet<TEntity>(TEntity fakeEntity1, TEntity fakeEntity2) where TEntity : class
    {
        return UseSet(new TEntity[2] { fakeEntity1, fakeEntity2 });
    }

    public DbContextMock<TDbContext> UseSet<TEntity>(TEntity fakeEntity1, TEntity fakeEntity2, TEntity fakeEntity3) where TEntity : class
    {
        return UseSet(new TEntity[3] { fakeEntity1, fakeEntity2, fakeEntity3 });
    }

    public DbContextMock<TDbContext> UseSet<TEntity>(params TEntity[] fakeCollection) where TEntity : class
    {
        return UseSet((IEnumerable<TEntity>)fakeCollection);
    }

    public IEnumerable<TEntity> Set<TEntity>() where TEntity : class
    {
        return Object.Set<TEntity>();
    }

    private Action<LogOutput> AddLogAction(ITestOutputHelper? outputHelper)
    {
        ITestOutputHelper outputHelper2 = outputHelper;
        return delegate (LogOutput log)
        {
            if (_loggingStarted)
            {
                if (outputHelper2 == null)
                {
                    _logs.Add(log);
                }
                else
                {
                    LogToOutput(outputHelper2, log);
                }
            }
        };
    }

    private static void LogToOutput(ITestOutputHelper outputHelper, LogOutput log)
    {
        if (outputHelper == null)
        {
            throw new ArgumentNullException("outputHelper");
        }

        outputHelper.WriteLine(log?.ToString() + Environment.NewLine);
    }

    public void DisposeObject()
    {
        _loggingStarted = false;
    }
}

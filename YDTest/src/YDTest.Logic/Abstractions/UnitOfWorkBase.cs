using YDTest.Data;

namespace YDTest.Logic.Abstractions;

public class UnitOfWorkBase : IDisposable
{
    protected readonly YDTestContext Context;

    public UnitOfWorkBase(YDTestContext context)
    {
        Context = context;
    }

    public void Save()
    {
        Context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}


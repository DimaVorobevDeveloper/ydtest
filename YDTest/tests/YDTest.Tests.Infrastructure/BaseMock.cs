using Moq;

namespace YDTest.Tests.Infrastructure;

public abstract class BaseMock<TInterface, TMock> : Mock<TInterface> where TInterface : class where TMock : BaseMock<TInterface, TMock>
{
    private bool _isBuilt;

    public override TInterface Object
    {
        get
        {
            if (!_isBuilt)
            {
                Build();
            }

            return base.Object;
        }
    }

    protected virtual void Create()
    {
    }

    public TMock Build()
    {
        Create();
        _isBuilt = true;
        return (TMock)this;
    }

    public TInterface RebuildObject()
    {
        Build();
        return base.Object;
    }

    public TMock ReturnsAsync<TResult>(TResult result)
    {
        SetReturnsDefault(Task.FromResult(result));
        return (TMock)this;
    }
}

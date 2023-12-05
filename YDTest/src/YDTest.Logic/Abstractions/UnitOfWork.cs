using YDTest.Data;
using YDTest.Data.Entities;

namespace YDTest.Logic.Abstractions;

public class UnitOfWork : UnitOfWorkBase
{
    private GenericRepository<User> _userRepository;
    private GenericRepository<Team> _teamRepository;
    private GenericRepository<UserTeam> _userTeamRepository;

    public UnitOfWork(YDTestContext context) : base(context)
    {
    }

    public GenericRepository<User> UserRepository
    {
        get
        {
            return _userRepository ??= new GenericRepository<User>(Context);
        }
    }

    public GenericRepository<Team> TeamRepository
    {
        get
        {
            return _teamRepository ??= new GenericRepository<Team>(Context);
        }
    }

    public GenericRepository<UserTeam> UserTeamRepository
    {
        get
        {
            return _userTeamRepository ??= new GenericRepository<UserTeam>(Context);
        }
    }

    //public GenericRepository<TEntity> GetEntityRepository<TEntity>(GenericRepository<TEntity> entityRepository) where TEntity : EntityBase
    //{
    //    return entityRepository ??= new GenericRepository<TEntity>(Context);
    //}
}
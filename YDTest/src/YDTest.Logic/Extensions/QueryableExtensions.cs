using YDTest.Data.Abstractions;

namespace YDTest.Logic.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<Entity> NotDeleted<Entity>(this IQueryable<Entity> entities)
     where Entity : EntityBase
    {
        return entities.Where(x => x.IsDeleted != true);
    }
}


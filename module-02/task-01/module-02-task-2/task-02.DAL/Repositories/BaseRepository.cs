using Module_02.Task_02.CatalogService.DAL.Context;

namespace Module_02.Task_02.CatalogService.DAL.Repositories;

public abstract class BaseRepository
{
    protected IDbSetContext DbContext { get; }

    protected BaseRepository(IDbSetContext dbContext)
    {
        DbContext = dbContext;
    }
}
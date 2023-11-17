using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Repositories;

public abstract class BaseRepository
{
    protected IDbSetContext DbContext { get; }

    protected BaseRepository(IDbSetContext dbContext)
    {
        DbContext = dbContext;
    }
}
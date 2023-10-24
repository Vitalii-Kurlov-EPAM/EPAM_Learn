using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;

public abstract class BaseHandler<TDbContext>
    where TDbContext : IDbContext
{
    protected TDbContext DbContext { get; }

    protected BaseHandler(TDbContext dbContext)
    {
        DbContext = dbContext;
    }
}
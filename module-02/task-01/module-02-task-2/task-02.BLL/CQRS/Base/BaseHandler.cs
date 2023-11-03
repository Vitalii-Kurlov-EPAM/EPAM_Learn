using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.Base;

public abstract class BaseHandler<TDbContext>
    where TDbContext : IDbContext
{
    protected TDbContext DbContext { get; }
    protected ILogger Logger { get; }

    protected BaseHandler(TDbContext dbContext, ILogger logger)
    {
        DbContext = dbContext;
        Logger = logger;
    }
}
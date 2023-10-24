using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.Base;

public abstract class BaseCommandHandler : BaseHandler<IWithModificationsDbContext>
{
    protected BaseCommandHandler(IWithModificationsDbContext dbContext)
        : base(dbContext)
    {
    }
}
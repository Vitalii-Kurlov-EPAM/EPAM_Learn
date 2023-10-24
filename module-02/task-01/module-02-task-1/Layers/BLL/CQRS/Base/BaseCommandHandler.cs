using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;

public abstract class BaseCommandHandler : BaseHandler<IDbContext>
{
    protected BaseCommandHandler(IDbContext dbContext)
        : base(dbContext)
    {
    }
}
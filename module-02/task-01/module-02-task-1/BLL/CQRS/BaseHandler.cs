using MediatR;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS;

public abstract class BaseHandler<TDbContext>
    where TDbContext : IDbContext
{
    protected IMediator Mediator { get; }
    protected TDbContext DbContext { get; }

    protected BaseHandler(IMediator mediator, TDbContext dbContext)
    {
        DbContext = dbContext;
        Mediator = mediator;
    }
}
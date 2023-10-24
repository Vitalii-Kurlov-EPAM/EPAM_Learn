using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;

public abstract class BaseQueryHandler : BaseHandler<IDbContext>
{
    protected IMediator Mediator { get; }

    protected BaseQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(dbContext)
    {
        Mediator = mediator;
    }
}
using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.Base;

public abstract class BaseQueryHandler : BaseHandler<IReadOnlyDbContext>
{
    protected IMediator Mediator { get; }

    protected BaseQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext)
        : base(dbContext)
    {
        Mediator = mediator;
    }
}
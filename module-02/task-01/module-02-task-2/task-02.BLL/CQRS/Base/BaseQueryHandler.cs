using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.Base;

public abstract class BaseQueryHandler : BaseHandler<IReadOnlyDbContext>
{
    protected IMediator Mediator { get; }

    protected BaseQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext, ILogger logger)
        : base(dbContext, logger)
    {
        Mediator = mediator;
    }
}
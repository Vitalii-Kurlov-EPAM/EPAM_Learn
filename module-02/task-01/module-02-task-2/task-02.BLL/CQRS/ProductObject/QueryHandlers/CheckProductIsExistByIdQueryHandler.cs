using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.QueryHandlers;

public sealed class CheckProductIsExistByIdQueryHandler : BaseQueryHandler,
    IRequestHandler<CheckProductIsExistByIdQuery, bool>
{
    public CheckProductIsExistByIdQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<bool> Handle(CheckProductIsExistByIdQuery query, CancellationToken cancellationToken)
    {
        return await DbContext.ProductRepository.IsExistByIdAsync(query.ProductId, cancellationToken);
    }
}
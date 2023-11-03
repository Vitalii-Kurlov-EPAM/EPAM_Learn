using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.QueryHandlers;

public sealed class GetProductByIdQueryHandler : BaseQueryHandler,
    IRequestHandler<GetProductByIdQuery, ProductObjectModels.ItemModel>
{
    public GetProductByIdQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext, ILogger<GetProductByIdQueryHandler> logger)
        : base(mediator, dbContext, logger)
    {
    }

    public async Task<ProductObjectModels.ItemModel> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product =
            await DbContext.ProductRepository.GetByIdAsync(query.ProductId, query.IncludeDeps,cancellationToken);
        return product.ToProductItemModel();
    }

}
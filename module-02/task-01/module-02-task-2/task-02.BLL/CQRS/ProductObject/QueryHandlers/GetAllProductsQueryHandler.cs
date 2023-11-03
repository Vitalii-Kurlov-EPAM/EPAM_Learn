using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.QueryHandlers;

public sealed class GetAllProductsQueryHandler : BaseQueryHandler, IRequestHandler<GetAllProductsQuery, ProductObjectModels.ItemModel[]>
{
    public GetAllProductsQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext, ILogger<GetAllProductsQueryHandler> logger)
        : base(mediator, dbContext, logger)
    {
    }

    public async Task<ProductObjectModels.ItemModel[]> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await DbContext.ProductRepository.GetAllAsync(query.IncludeDeps, cancellationToken);
        return products.Select(entity => entity.ToProductItemModel()).ToArray();
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.QueryHandlers;

public sealed class GetProductsByCategoryIdQueryHandler : BaseQueryHandler,
    IRequestHandler<GetProductsByCategoryIdQuery, PagedResult<ProductObjectModels.ItemModel>>
{
    public GetProductsByCategoryIdQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext, ILogger<GetProductsByCategoryIdQueryHandler> logger)
        : base(mediator, dbContext, logger)
    {
    }

    public async Task<PagedResult<ProductObjectModels.ItemModel>> Handle(GetProductsByCategoryIdQuery query,
        CancellationToken cancellationToken)
    {
        var products =
            await DbContext.ProductRepository.GetProductsByCategoryAsync(query.CategoryId, query.Page, query.PageSize, query.IncludeDeps, cancellationToken);
 
        return products.ToPagedProductItemsModel();
    }
}
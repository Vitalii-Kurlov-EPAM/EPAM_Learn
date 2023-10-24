using Module_02.Task_02.CatalogService.Abstractions.CQRS.Common.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB;

namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Queries;

public sealed record GetProductsByCategoryIdQuery(int CategoryId, int Page, int PageSize, bool IncludeDeps = false) : IncludeDepsQuery(IncludeDeps), IQueryRequest<PagedResult<ProductObjectModels.ItemModel>>;
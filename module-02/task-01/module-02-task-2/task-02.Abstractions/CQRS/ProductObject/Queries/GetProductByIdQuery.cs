using Module_02.Task_02.CatalogService.Abstractions.CQRS.Common.Queries;

namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Queries;

public sealed record GetProductByIdQuery(int ProductId, bool IncludeDeps = false) : IncludeDepsQuery(IncludeDeps), IQueryRequest<ProductObjectModels.ItemModel>;
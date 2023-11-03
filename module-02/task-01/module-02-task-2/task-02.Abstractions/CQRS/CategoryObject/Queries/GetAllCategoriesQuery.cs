using Module_02.Task_02.CatalogService.Abstractions.CQRS.Common.Queries;

namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Queries;

public sealed record GetAllCategoriesQuery(bool IncludeDeps = false) : IncludeDepsQuery(IncludeDeps), IQueryRequest<CategoryObjectModels.ItemModel[]>;
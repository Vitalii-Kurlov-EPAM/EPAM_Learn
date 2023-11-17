namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Queries;

public sealed record CheckCategoryIsExistByIdQuery(int CategoryId) : IQueryRequest<bool>;
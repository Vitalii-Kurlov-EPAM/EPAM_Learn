namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Queries;

public sealed record CheckProductIsExistByIdQuery(int ProductId) : IQueryRequest<bool>;
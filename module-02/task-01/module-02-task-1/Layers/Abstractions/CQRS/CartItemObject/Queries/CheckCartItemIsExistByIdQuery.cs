namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;

public sealed record CheckCartItemIsExistByIdQuery(int CartItemId) : IQueryRequest<bool>;

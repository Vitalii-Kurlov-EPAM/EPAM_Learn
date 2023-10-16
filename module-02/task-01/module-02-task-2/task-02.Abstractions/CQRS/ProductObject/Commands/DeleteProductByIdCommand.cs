namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;

public sealed record DeleteProductByIdCommand(int ProductId, int Amount) : ICommandRequest<bool>;
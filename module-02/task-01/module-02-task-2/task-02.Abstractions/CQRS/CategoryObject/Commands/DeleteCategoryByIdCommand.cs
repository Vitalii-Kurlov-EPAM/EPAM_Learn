namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;

public sealed record DeleteCategoryByIdCommand(int CategoryId) : ICommandRequest<bool>;
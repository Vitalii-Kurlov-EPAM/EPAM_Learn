namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;

public sealed record UpdateCategoryCommand : ICommandRequest<bool>
{
    public required int CategoryId { get; init; }

    public required string Name { get; init; }

    public required string Image { get; init; }

    public required int? ParentCategoryId { get; init; }
}
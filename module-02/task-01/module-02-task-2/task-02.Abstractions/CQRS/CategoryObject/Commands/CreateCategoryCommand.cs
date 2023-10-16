namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;

public sealed record CreateCategoryCommand : ICommandRequest<CategoryObjectModels.ItemModel>
{
    public required string Name { get; init; }

    public required string Image { get; init; }

    public required int? ParentCategoryId { get; init; }
}
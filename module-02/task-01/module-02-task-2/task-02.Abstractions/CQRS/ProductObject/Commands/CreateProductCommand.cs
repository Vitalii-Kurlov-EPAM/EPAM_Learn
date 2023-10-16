namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;

public sealed record CreateProductCommand : ICommandRequest<ProductObjectModels.ItemModel>
{
    public required int CategoryId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Image { get; init; }
    public required decimal Price { get; init; }
    public required int Amount { get; init; }
}
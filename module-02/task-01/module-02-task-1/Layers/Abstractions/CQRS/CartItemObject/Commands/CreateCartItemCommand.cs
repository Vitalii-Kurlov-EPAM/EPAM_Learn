namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;

public sealed record CreateCartItemCommand : ICommandRequest<CartItemObjectModels.ItemModel>
{
    #region Item's data

    public required int CartId { get; init; }
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }

    #endregion

    #region Image's data

    public required string ImageUrl { get; init; }
    public required string ImageAlt { get; init; }

    #endregion
}
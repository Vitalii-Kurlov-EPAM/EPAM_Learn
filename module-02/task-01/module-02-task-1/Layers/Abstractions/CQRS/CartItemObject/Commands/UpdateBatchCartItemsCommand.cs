namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;

public sealed record UpdateBatchCartItemsCommand : ICommandRequest<bool>
{
    #region Item's data

    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }

    #endregion

    #region Image's data

    public required string ImageUrl { get; init; }

    #endregion
}
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;

internal static class CartItemModelExtensions
{
    public static CartItemEntity ToCartItemEntity(this CreateCartItemCommand value, ImageEntity image)
        => new()
        {
            CartItemId = 0,
            CartId = value.CartId,
            Name = value.Name,
            Price = value.Price,
            Quantity = value.Quantity,
            Id = value.Id,
            Image = image
        };


    public static CartItemObjectModels.ItemModel ToCartItemModel(this CartItemEntity value)
        => value == null ? null : new CartItemObjectModels.ItemModel()
        {
            CartItemId = value.CartItemId,
            CartId = value.CartId,
            Name = value.Name,
            Price = value.Price,
            Quantity = value.Quantity,
            Id = value.Id,
            Image = value.Image.ToImageItemModel()
        };
}
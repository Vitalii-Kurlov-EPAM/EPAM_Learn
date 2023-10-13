using Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Commands;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;
using Module_02.Task_01.CartingService.WebApi.DAL.Entities;

namespace Module_02.Task_01.CartingService.WebApi.BLL.MappingExtensions;

internal static class CartItemModelExtensions
{
    public static CreateImageCommand ToCreateImageCommand(this CreateCartItemCommand value) 
        => new()
        {
            Alt = value.ImageAlt,
            Url = value.ImageUrl
        };

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


    public static CartItem.ItemModel ToCartItemModel(this CartItemEntity value)
        => value == null ? null : new CartItem.ItemModel()
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
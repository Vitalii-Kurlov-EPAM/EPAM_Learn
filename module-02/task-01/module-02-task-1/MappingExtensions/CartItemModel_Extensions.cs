using Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;
using Module_02.Task_01.CartingService.WebApi.Models.CartItem;

namespace Module_02.Task_01.CartingService.WebApi.MappingExtensions;

public static class CartItemModelExtensions
{
    public static CartItemResponse.ItemDto ToCartItemResponseDto(this CartItem.ItemModel value) 
        => value == null ? null : new CartItemResponse.ItemDto
        {
            CartItemId = value.CartItemId,
            CartId = value.CartId,
            Name = value.Name,
            Price = value.Price,
            Quantity = value.Quantity,
            Id = value.Id,
            
            ImageId = value.Image?.ImageId,
            ImageAlt = value.Image?.ImageAlt,
            ImageUrl = value.Image?.ImageUrl,
        };

    public static CreateCartItemCommand ToCreateCartItemCommand(this CartItemRequest.CreateModel value, int cartId)
    {
        return new CreateCartItemCommand
        {
            CartId = cartId,
            Id = value.Id,
            Name = value.Name,
            Price = value.Price,
            Quantity = value.Quantity,

            ImageAlt = value.ImageAlt,
            ImageUrl = value.ImageUrl
        };
    }
}
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Models.CartItem;

namespace Module_02.Task_01.CartingService.WebApi.MappingExtensions;

public static class CartItemModelExtensions
{
    public static CartItemResponse.ItemDto ToCartItemResponseDto(this CartItemObjectModels.ItemModel value) 
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


    public static CartItemResponse.CartItemsDto ToCartItemsResponseDto(this CartItemObjectModels.ItemModel[] value,
        string cartId)
        => value == null
            ? null
            : new CartItemResponse.CartItemsDto
            {
                CartId = cartId,
                Items = value.Select(model => model.ToCartItemResponseDto()).ToArray()
            };
    

    public static CreateCartItemCommand ToCreateCartItemCommand(this CartItemRequest.CreateModel value, string cartId)
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
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;
using Module_02.Task_01.CartingService.WebApi.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.Models.CartItem;

namespace Module_02.Task_01.CartingService.WebApi.Controllers
{
    public partial class CartsController
    {
        /// <summary>
        /// Gets All items in cart V2
        /// </summary>
        /// <param name="cartId">Cart ID for what to get items</param>
        [HttpGet("{cartId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartItemResponse.ItemDto[]))]
        [MapToApiVersion(2.0)]
        public async Task<IActionResult> GetAllV2(string cartId)
        {
            var allCartItems = await _mediator.Send(new GetAllCartItemsQuery(cartId, true));
            return Ok(allCartItems.Select(model => model.ToCartItemResponseDto()));
        }
    }
}
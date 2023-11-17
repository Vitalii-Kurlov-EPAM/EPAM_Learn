using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;
using Module_02.Task_01.CartingService.WebApi.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.Models;
using Module_02.Task_01.CartingService.WebApi.Models.CartItem;
using Module_02.Task_01.CartingService.WebApi.Static;

namespace Module_02.Task_01.CartingService.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModelResponse))]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    public partial class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets All items in cart V1
        /// </summary>
        /// <param name="cartId">Cart ID for what to get items</param>
        [HttpGet("{cartId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartItemResponse.CartItemsDto))]
        [MapToApiVersion(1.0)]
        [Authorize(Policy = PolicyName.CARTS_READ)]
        public async Task<IActionResult> GetAll(string cartId)
        {
            var allCartItems = await _mediator.Send(new GetAllCartItemsQuery(cartId, true));
            return Ok(allCartItems.ToCartItemsResponseDto(cartId));
        }

        [HttpGet("{cartId}/items/{itemId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartItemResponse.ItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModelResponse))]
        [Authorize(Policy = PolicyName.CARTS_READ)]
        public async Task<IActionResult> GetById(string cartId, int itemId)
        {
            var cartItem = await _mediator.Send(new GetCartItemByIdQuery(cartId, itemId, true));
            return cartItem == null
                ? NotFound()
                : Ok(cartItem.ToCartItemResponseDto());
        }

        [HttpPost("{cartId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModelResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModelResponse))]
        [Authorize(Policy = PolicyName.CARTS_CREATE)]
        public async Task<IActionResult> Create(string cartId, CartItemRequest.CreateModel model)
        {
            var cartItem = await _mediator.Send(model.ToCreateCartItemCommand(cartId));
            return Ok(cartItem);
        }

        [HttpDelete("{cartId}/items/{itemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModelResponse))]
        [Authorize(Policy = PolicyName.CARTS_DELETE)]
        public async Task<IActionResult> Delete(string cartId, int itemId, [FromQuery, Required]int quantity)
        {
            var result = await _mediator.Send(new DeleteCartItemByIdCommand(cartId, itemId, quantity));
            return result
                ? Ok()
                : NotFound();
        }
    }
}
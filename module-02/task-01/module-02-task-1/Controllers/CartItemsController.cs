using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;
using Module_02.Task_01.CartingService.WebApi.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Models.CartItem;

namespace Module_02.Task_01.CartingService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{cartId:int}/items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CartItemResponse.ItemDto>))]
        public async Task<IActionResult> GetAll(int cartId)
        {
            var allCartItems = await _mediator.Send(new GetAllCartItemsQuery(cartId, true));
            return Ok(allCartItems.Select(model => model.ToCartItemResponseDto()));
        }

        [HttpGet("{cartId:int}/items/{cartItemId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartItemResponse.ItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int cartId, int cartItemId)
        {
            var cartItem = await _mediator.Send(new GetCartItemByIdQuery(cartId, cartItemId, true));
            return cartItem == null
                ? NotFound()
                : Ok(cartItem.ToCartItemResponseDto());
        }

        [HttpPost("{cartId:int}/items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(int cartId, CartItemRequest.CreateModel model)
        {
            var cartItemId = await _mediator.Send(model.ToCreateCartItemCommand(cartId));
            return CreatedAtAction("GetById", new
            {
                cartId = cartId,
                cartItemId = cartItemId
            }, null);
        }

        [HttpDelete("{cartId:int}/items/{cartItemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int cartId, int cartItemId, [FromQuery, Required]int quantity)
        {
            var result = await _mediator.Send(new DeleteCartItemByIdCommand(cartId, cartItemId, quantity));
            return result
                ? NoContent()
                : NotFound();
        }
    }
}
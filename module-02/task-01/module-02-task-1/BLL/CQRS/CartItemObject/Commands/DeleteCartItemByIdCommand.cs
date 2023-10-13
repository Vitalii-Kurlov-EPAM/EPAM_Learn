using MediatR;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Commands;

public sealed class DeleteCartItemByIdCommand : IRequest<bool>
{
    public int CartId { get; }
    public int CartItemId { get; }
    public int Quantity { get; }

    public DeleteCartItemByIdCommand(int cartId, int cartItemId, int quantity)
    {
        CartItemId = cartItemId;
        Quantity = quantity;
        CartId = cartId;
    }
}
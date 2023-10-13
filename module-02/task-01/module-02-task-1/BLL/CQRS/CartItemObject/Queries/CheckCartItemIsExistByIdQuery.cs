using MediatR;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;

public sealed class CheckCartItemIsExistByIdQuery : IRequest<bool>
{
    public int CartItemId { get; }

    public CheckCartItemIsExistByIdQuery(int cartItemId)
    {
        CartItemId = cartItemId;
    }
}
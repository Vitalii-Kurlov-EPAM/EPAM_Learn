using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;

public sealed class GetCartItemByIdQuery : IRequest<CartItem.ItemModel>
{
    public int CartId { get; }
    public int CartItemId { get; }
    public bool IncludeDependencies { get; }
   
    public GetCartItemByIdQuery(int cartId, int cartItemId, bool includeDependencies)
    {
        CartId = cartId;
        IncludeDependencies = includeDependencies;
        CartItemId = cartItemId;
    }
}
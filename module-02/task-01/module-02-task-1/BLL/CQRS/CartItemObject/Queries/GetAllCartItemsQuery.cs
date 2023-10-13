using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;

public sealed class GetAllCartItemsQuery : IRequest<CartItem.ItemModel[]>
{
    public int CartId { get; }
    public bool IncludeDependencies { get; }
   
    public GetAllCartItemsQuery(int cartId, bool includeDependencies)
    {
        IncludeDependencies = includeDependencies;
        CartId = cartId;
    }
}
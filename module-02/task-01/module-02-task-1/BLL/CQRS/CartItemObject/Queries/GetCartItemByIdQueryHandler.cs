using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;

public sealed class GetCartItemByIdQueryHandler : BaseCartItemQueryHandler,
    IRequestHandler<GetCartItemByIdQuery, CartItem.ItemModel>
{
    public GetCartItemByIdQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<CartItem.ItemModel> Handle(GetCartItemByIdQuery query, CancellationToken cancellationToken)
    {
        var cartItemsDbSet = query.IncludeDependencies 
            ? CartItemsDbSet.Include(x => x.Image)
            : CartItemsDbSet;

        var foundItem = cartItemsDbSet.Where(entity => entity.CartId == query.CartId && entity.CartItemId == query.CartItemId).FirstOrDefault();

        return Task.FromResult(foundItem.ToCartItemModel());
    }
}
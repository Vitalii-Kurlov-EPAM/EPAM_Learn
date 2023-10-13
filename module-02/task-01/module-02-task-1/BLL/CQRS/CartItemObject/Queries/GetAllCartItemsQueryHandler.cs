using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;

public sealed class GetAllCartItemsQueryHandler : BaseCartItemQueryHandler,
    IRequestHandler<GetAllCartItemsQuery, CartItem.ItemModel[]>
{
    public GetAllCartItemsQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<CartItem.ItemModel[]> Handle(GetAllCartItemsQuery query, CancellationToken cancellationToken)
    {
        var itemsDbSet = query.IncludeDependencies 
            ? CartItemsDbSet.Include(x => x.Image)
            : CartItemsDbSet;

        var foundItems = itemsDbSet.Where(entity => entity.CartId == query.CartId);

        var resultItems = foundItems.Select(entity => entity.ToCartItemModel()).ToArray();

        return Task.FromResult(resultItems);
    }
}
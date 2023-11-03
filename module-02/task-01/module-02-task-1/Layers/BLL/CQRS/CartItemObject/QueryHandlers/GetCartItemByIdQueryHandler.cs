using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.CartItemObject.QueryHandlers;

public sealed class GetCartItemByIdQueryHandler : BaseQueryHandler,
    IRequestHandler<GetCartItemByIdQuery, CartItemObjectModels.ItemModel>
{
    public GetCartItemByIdQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<CartItemObjectModels.ItemModel> Handle(GetCartItemByIdQuery query, CancellationToken cancellationToken)
    {
        var foundItem = DbContext.CartItemRepository.GetCartItem(query.CartId, query.ItemId, query.IncludeDeps);
        return Task.FromResult(foundItem.ToCartItemModel());
    }
}
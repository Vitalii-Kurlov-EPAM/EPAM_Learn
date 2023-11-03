using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.CartItemObject.QueryHandlers;

public sealed class GetAllCartItemsQueryHandler : BaseQueryHandler,
    IRequestHandler<GetAllCartItemsQuery, CartItemObjectModels.ItemModel[]>
{
    public GetAllCartItemsQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<CartItemObjectModels.ItemModel[]> Handle(GetAllCartItemsQuery query, CancellationToken cancellationToken)
    {
        var foundItems = DbContext.CartItemRepository.GetAllCartItems(query.CartId, query.IncludeDeps);

        var resultItems = foundItems.Select(entity => entity.ToCartItemModel()).ToArray();

        return Task.FromResult(resultItems);
    }
}
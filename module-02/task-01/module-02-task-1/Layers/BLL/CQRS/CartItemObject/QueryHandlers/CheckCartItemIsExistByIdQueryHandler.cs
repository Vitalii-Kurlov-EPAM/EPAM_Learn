using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.CartItemObject.QueryHandlers;

public sealed class CheckCartItemIsExistByIdQueryHandler : BaseQueryHandler,
    IRequestHandler<CheckCartItemIsExistByIdQuery, bool>
{
    public CheckCartItemIsExistByIdQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<bool> Handle(CheckCartItemIsExistByIdQuery query, CancellationToken cancellationToken)
    {
       var isExist = DbContext.CartItemRepository.IsExistById(query.CartItemId);
       return Task.FromResult(isExist);
    }
}
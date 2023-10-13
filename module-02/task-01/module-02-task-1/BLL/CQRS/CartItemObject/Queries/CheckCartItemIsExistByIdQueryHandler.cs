using MediatR;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;

public sealed class CheckCartItemIsExistByIdQueryHandler : BaseCartItemQueryHandler,
    IRequestHandler<CheckCartItemIsExistByIdQuery, bool>
{
    public CheckCartItemIsExistByIdQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<bool> Handle(CheckCartItemIsExistByIdQuery query, CancellationToken cancellationToken)
    {
        var isExist = CartItemsDbSet.Any(entity => entity.CartItemId == query.CartItemId);
        return Task.FromResult(isExist);
    }
}
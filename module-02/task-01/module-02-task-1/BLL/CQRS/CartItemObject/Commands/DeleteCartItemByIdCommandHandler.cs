using LiteDB;
using MediatR;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;
using Сommon.DB.Exceptions;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Commands;

public sealed class DeleteCartItemByIdCommandHandler : BaseCartItemCommandHandler,
    IRequestHandler<DeleteCartItemByIdCommand, bool>
{
    public DeleteCartItemByIdCommandHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<bool> Handle(DeleteCartItemByIdCommand command, CancellationToken cancellationToken)
    {
        var foundCartItem = CartItemsDbSet
                                .Where(entity =>
                                    entity.CartId == command.CartId && entity.CartItemId == command.CartItemId)
                                .FirstOrDefault()
                            ?? throw new DbRecordNotFoundException(
                                $"Can not delete Cart Item. The passed cart_item_id={command.CartItemId} not found.");

        bool deleted;

        if (foundCartItem.Quantity > command.Quantity)
        {
            foundCartItem.Quantity -= command.Quantity;
            deleted = CartItemsDbSet.Update(foundCartItem);
        }
        else
        {
            deleted = CartItemsDbSet.Delete(CartItemsDbSet);
        }

        if (!deleted)
        {
            throw new DbRecordNotFoundException(
                $"Can not delete items from cart. Record with ID={command.CartItemId} not found.");
        }

        return Task.FromResult(true);
    }
}
using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Exceptions;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.CartItemObject.CommandHandlers;

public sealed class DeleteCartItemByIdCommandHandler : BaseCommandHandler,
    IRequestHandler<DeleteCartItemByIdCommand, bool>
{
    public DeleteCartItemByIdCommandHandler(IDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<bool> Handle(DeleteCartItemByIdCommand command, CancellationToken cancellationToken)
    {
        var foundCartItem = DbContext.CartItemRepository.GetCartItem(command.CartId, command.ItemId, false)
                            ?? throw new DbRecordNotFoundException(
                                $"Can not delete Cart Item. The passed item_id={command.ItemId} not found.");

        bool deleted;

        if (foundCartItem.Quantity > command.Quantity)
        {
            foundCartItem.Quantity -= command.Quantity;
            deleted = DbContext.CartItemRepository.Update(foundCartItem);
        }
        else
        {
            deleted = DbContext.CartItemRepository.Delete(foundCartItem);
        }

        if (!deleted)
        {
            throw new DbRecordNotFoundException(
                $"Can not delete items from cart. Record with ID={command.ItemId} not found.");
        }

        return Task.FromResult(true);
    }
}
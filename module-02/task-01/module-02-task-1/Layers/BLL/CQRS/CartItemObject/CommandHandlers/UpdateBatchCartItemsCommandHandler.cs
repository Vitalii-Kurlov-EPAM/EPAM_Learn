using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Utils;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.CartItemObject.CommandHandlers;

public class UpdateBatchCartItemsCommandHandler : BaseCommandHandler,
    IRequestHandler<UpdateBatchCartItemsCommand, bool>
{
    public UpdateBatchCartItemsCommandHandler(IDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<bool> Handle(UpdateBatchCartItemsCommand command, CancellationToken cancellationToken)
    {
        var foundItems = DbContext.CartItemRepository.GetAllCartItemsByItemId(command.Id, true).ToArray();
        var result = false;

        if (DbContext.BeginTransaction())
        {
            foreach (var item in foundItems)
            {
                item.Name = command.Name;
                item.Price = command.Price;
                DbContext.CreateOrUpdateImage(item, command.ImageUrl, null);
                DbContext.CartItemRepository.Update(item);
            }

            result = DbContext.CommitChanges();
        }

        return Task.FromResult(result);
    }
}
using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.CommandHandlers;

public sealed class DeleteProductByIdCommandHandler : BaseCommandHandler,
    IRequestHandler<DeleteProductByIdCommand, bool>
{
    public DeleteProductByIdCommandHandler(IWithModificationsDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
    {
        var foundProduct =
            await DbContext.ProductRepository.GetByIdAsync(command.ProductId, false, cancellationToken)
            ?? throw new DbRecordNotFoundException(
                $"Can not delete Product. The passed product ID={command.ProductId} not found.");

        if (foundProduct.Amount > command.Amount)
        {
            foundProduct.Amount -= command.Amount;
            await DbContext.ProductRepository.UpdateAsync(foundProduct, cancellationToken);
        }
        else
        {
            await DbContext.ProductRepository.DeleteAsync(foundProduct, cancellationToken);
        }

        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
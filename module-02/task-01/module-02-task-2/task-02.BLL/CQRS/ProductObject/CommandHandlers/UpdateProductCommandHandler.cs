using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.CommandHandlers;

public sealed class UpdateProductCommandHandler : BaseCommandHandler, IRequestHandler<UpdateProductCommand, bool>
{
    public UpdateProductCommandHandler(IWithModificationsDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (await DbContext.ProductRepository.IsExistByNameAsync(command.Name, cancellationToken))
        {
            throw new DbRecordAlreadyExistException($"Can not update product name with \"{command.Name}\". This name is already exist.");
        }

        if (!await DbContext.CategoryRepository.IsExistByIdAsync(command.CategoryId, cancellationToken))
        {
            throw new DbRecordNotFoundException(
                $"Can not update Product. The passed category ID={command.CategoryId} not found.");
        }

        var foundProduct =
            await DbContext.ProductRepository.GetByIdAsync(command.ProductId, false, cancellationToken) 
            ?? throw new DbRecordNotFoundException(
                $"Can not update Product. The passed product ID={command.ProductId} not found.");
        
        foundProduct.CategoryId = command.CategoryId;
        foundProduct.Name = command.Name;
        foundProduct.Description = command.Description;
        foundProduct.Image = command.Image;
        foundProduct.Price = command.Price;
        foundProduct.Amount = command.Amount;

        await DbContext.ProductRepository.UpdateAsync(foundProduct, cancellationToken);

        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
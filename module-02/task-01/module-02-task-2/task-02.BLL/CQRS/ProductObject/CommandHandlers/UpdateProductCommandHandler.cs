using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;
using Module_02.Task_02.CatalogService.Abstractions.Services.MessageProducers;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;
using Module_02.Task_02.CatalogService.BLL.ModelExtensions;
using Module_02.Task_02.CatalogService.BLL.Static;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.CommandHandlers;

public sealed class UpdateProductCommandHandler : BaseEntityModificationCommandHandler, IRequestHandler<UpdateProductCommand, bool>
{
    public UpdateProductCommandHandler(IWithModificationsDbContext dbContext, 
        ILogger<UpdateProductCommandHandler> logger,
        IEntityMessageProducer entityMessageProducer)
        : base(dbContext, logger, entityMessageProducer)
    {
    }

    public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (await DbContext.ProductRepository.IsExistByNameAsync(command.Name, command.ProductId, cancellationToken))
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

        if (affectedCount > 0)
        {
            EntityMessageProducer.PublishEntityUpdated(foundProduct.ToProductMessage(), EntityName.PRODUCT);
        }

        return affectedCount > 0;
    }
}
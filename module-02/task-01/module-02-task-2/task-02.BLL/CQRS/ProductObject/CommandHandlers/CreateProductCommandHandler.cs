using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;
using Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;
using Module_02.Task_02.CatalogService.Abstractions.Services.MessageProducers;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;
using Module_02.Task_02.CatalogService.BLL.ModelExtensions;
using Module_02.Task_02.CatalogService.BLL.Static;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.ProductObject.CommandHandlers;

public sealed class CreateProductCommandHandler : BaseEntityModificationCommandHandler, IRequestHandler<CreateProductCommand, ProductObjectModels.ItemModel>
{
    public CreateProductCommandHandler(
        IWithModificationsDbContext dbContext,
        IEntityMessageProducer entityMessageProducer,
        ILogger<CreateProductCommandHandler> logger)
        : base(dbContext, logger, entityMessageProducer)
    {
    }

    public async Task<ProductObjectModels.ItemModel> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (await DbContext.ProductRepository.IsExistByNameAsync(command.Name, cancellationToken))
        {
            throw new DbRecordAlreadyExistException($"Can not add a new product with name=\"{command.Name}\". This name is already exist.");
        }
        
        var isCategoryExist = await DbContext.CategoryRepository.IsExistByIdAsync(command.CategoryId, cancellationToken);
        
        if (!isCategoryExist)
        {
            throw new DbRecordNotFoundException(
                $"Can not add a new product. The passed category ID={command.CategoryId} not found.");
        }

        var productEntity = command.ToProductEntity();
        await DbContext.ProductRepository.AddAsync(productEntity, cancellationToken);
        
        await DbContext.SaveChangesAsync(cancellationToken);

        EntityMessageProducer.PublishEntityAdded(productEntity.ToProductMessage(), EntityName.PRODUCT);

        return productEntity.ToProductItemModel();
    }
}
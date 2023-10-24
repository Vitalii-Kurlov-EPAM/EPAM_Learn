using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;
using Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.CategoryObject.CommandHandlers;

public sealed class CreateCategoryCommandHandler : BaseCommandHandler,
    IRequestHandler<CreateCategoryCommand, CategoryObjectModels.ItemModel>
{
    public CreateCategoryCommandHandler(IWithModificationsDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<CategoryObjectModels.ItemModel> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (await DbContext.CategoryRepository.IsExistByNameAsync(command.Name, cancellationToken))
        {
            throw new DbRecordAlreadyExistException($"Can not add a new category with name=\"{command.Name}\". This name is already exist.");
        }

        var categoryEntity = command.ToCategoryEntity();

        if (command.ParentCategoryId != null)
        {
            var isParentCategoryExist =
                await DbContext.CategoryRepository.IsExistByIdAsync(command.ParentCategoryId.Value, cancellationToken);
            
            if (!isParentCategoryExist)
            {
                throw new DbRecordNotFoundException(
                    $"Can not add a new category. The passed parent category ID={command.ParentCategoryId} not found.");
            }
        }

        await DbContext.CategoryRepository.AddAsync(categoryEntity, cancellationToken);

        await DbContext.SaveChangesAsync(cancellationToken);

        return categoryEntity.ToCategoryItemModel();
    }
}
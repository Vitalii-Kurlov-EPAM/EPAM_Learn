using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.CategoryObject.CommandHandlers;

public sealed class UpdateCategoryCommandHandler : BaseCommandHandler, IRequestHandler<UpdateCategoryCommand, bool>
{
    public UpdateCategoryCommandHandler(IWithModificationsDbContext dbContext, ILogger<UpdateCategoryCommandHandler> logger)
        : base(dbContext, logger)
    {
    }

    public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (await DbContext.CategoryRepository.IsExistByNameAsync(command.Name, cancellationToken))
        {
            throw new DbRecordAlreadyExistException($"Can not update category name with \"{command.Name}\". This name is already exist.");
        }

        if (command.ParentCategoryId != null)
        {
            var isParentCategoryExist =
                await DbContext.CategoryRepository.IsExistByIdAsync(command.ParentCategoryId.Value, cancellationToken);
                
            if (!isParentCategoryExist)
            {
                throw new DbRecordNotFoundException(
                    $"Can not update category. The passed parent category ID={command.ParentCategoryId} not found.");
            }
        }

        var foundCategory = await DbContext.CategoryRepository.GetByIdAsync(command.CategoryId, false, cancellationToken);

        if (foundCategory == null)
        {
            throw new DbRecordNotFoundException(
                $"Can not update category. The passed category ID={command.CategoryId} not found.");
        }

        foundCategory.Name = command.Name;
        foundCategory.Image = command.Image;
        foundCategory.ParentCategoryId = command.ParentCategoryId;

        await DbContext.CategoryRepository.UpdateAsync(foundCategory, cancellationToken);

        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
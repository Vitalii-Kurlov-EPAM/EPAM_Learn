using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.CategoryObject.CommandHandlers;

public sealed class DeleteCategoryByIdCommandHandler : BaseCommandHandler,
    IRequestHandler<DeleteCategoryByIdCommand, bool>
{
    public DeleteCategoryByIdCommandHandler(IWithModificationsDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<bool> Handle(DeleteCategoryByIdCommand command, CancellationToken cancellationToken)
    {
        var foundCategory =
            await DbContext.CategoryRepository.GetByIdAsync(command.CategoryId, false, cancellationToken);

        if (foundCategory == null)
        {
            throw new DbRecordNotFoundException(
                $"Can not delete category. The passed category ID={command.CategoryId} not found.");
        }

        await DbContext.CategoryRepository.DeleteAsync(foundCategory, cancellationToken);
 
        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
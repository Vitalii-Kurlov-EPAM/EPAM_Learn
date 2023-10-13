using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;
using Сommon.DB.Exceptions;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;

public sealed class DeleteCategoryByIdCommandHandler : BaseCategoryCommandHandler,
    IRequestHandler<DeleteCategoryByIdCommand, bool>
{
    public DeleteCategoryByIdCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<bool> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        var foundCategory =
            await CategoriesDbSet.SingleOrDefaultAsync(entity => entity.CategoryId == request.CategoryId,
                cancellationToken);

        if (foundCategory == null)
        {
            throw new DbRecordNotFoundException(
                $"Can not delete Category. The passed category_id={request.CategoryId} not found.");
        }

        CategoriesDbSet.Remove(foundCategory);
        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
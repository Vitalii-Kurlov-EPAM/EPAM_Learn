using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;
using Module_02.Task_02.DAL.Context;
using Сommon.DB.Exceptions;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;

public sealed class UpdateCategoryCommandHandler : BaseCategoryCommandHandler, IRequestHandler<UpdateCategoryCommand, bool>
{
    public UpdateCategoryCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentCategoryId != null)
        {
            var isParentCategoryExist = await Mediator.Send(new CheckCategoryIsExistByIdQuery(request.ParentCategoryId.Value), cancellationToken);
            if (!isParentCategoryExist)
            {
                throw new DbRecordNotFoundException(
                    $"Can not update Category. The passed parent category_id={request.ParentCategoryId} not found.");
            }
        }

        var foundCategory =
            await CategoriesDbSet.SingleOrDefaultAsync(entity => entity.CategoryId == request.CategoryId,
                cancellationToken);

        if (foundCategory == null)
        {
            throw new DbRecordNotFoundException(
                $"Can not update Category. The passed category_id={request.CategoryId} not found.");
        }

        foundCategory.Name = request.Name;
        foundCategory.Image = request.Image;
        foundCategory.ParentCategoryId = request.ParentCategoryId;

        CategoriesDbSet.Update(foundCategory);

        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
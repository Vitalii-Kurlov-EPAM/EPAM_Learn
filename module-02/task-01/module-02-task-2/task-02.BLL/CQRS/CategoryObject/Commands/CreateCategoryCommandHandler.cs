using MediatR;
using Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;
using Module_02.Task_02.BLL.MappingExtensions;
using Module_02.Task_02.DAL.Context;
using Сommon.DB.Exceptions;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;

public sealed class CreateCategoryCommandHandler : BaseCategoryCommandHandler,
    IRequestHandler<CreateCategoryCommand, int>
{
    public CreateCategoryCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryEntity = request.ToCategoryEntity();

        if (request.ParentCategoryId != null)
        {
            var isParentCategoryExist =
                await Mediator.Send(new CheckCategoryIsExistByIdQuery(request.ParentCategoryId.Value),
                    cancellationToken);
            if (!isParentCategoryExist)
            {
                throw new DbRecordNotFoundException(
                    $"Can not add a new Category. The passed parent category_id={request.ParentCategoryId} not found.");
            }
        }

        await CategoriesDbSet.AddAsync(categoryEntity, cancellationToken);
        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0
            ? Convert.ToInt32(categoryEntity.CategoryId)
            : 0;
    }
}
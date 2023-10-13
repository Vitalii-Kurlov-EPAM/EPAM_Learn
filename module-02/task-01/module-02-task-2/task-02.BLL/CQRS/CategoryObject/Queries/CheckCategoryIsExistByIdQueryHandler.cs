using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;

public sealed class CheckCategoryIsExistByIdQueryHandler : BaseCategoryQueryHandler,
    IRequestHandler<CheckCategoryIsExistByIdQuery, bool>
{
    public CheckCategoryIsExistByIdQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<bool> Handle(CheckCategoryIsExistByIdQuery query, CancellationToken cancellationToken)
    {
        return await CategoriesDbSet.AnyAsync(entity => entity.CategoryId == query.CategoryId, cancellationToken);
    }
}
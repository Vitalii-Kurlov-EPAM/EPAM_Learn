using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.BLL.MappingExtensions;
using Module_02.Task_02.BLL.Models;
using Module_02.Task_02.DAL.Context;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;

public sealed class GetCategoryByIdQueryHandler : BaseCategoryQueryHandler,
    IRequestHandler<GetCategoryByIdQuery, Category.ItemModel>
{
    public GetCategoryByIdQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<Category.ItemModel> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category =
            await CategoriesDbSet.SingleOrDefaultAsync(entity => entity.CategoryId == query.CategoryId,
                cancellationToken);
        return category.ToCategoryItemModel();
    }

}
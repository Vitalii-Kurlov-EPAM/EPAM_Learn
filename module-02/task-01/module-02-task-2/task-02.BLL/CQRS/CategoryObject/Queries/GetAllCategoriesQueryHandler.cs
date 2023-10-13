using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.BLL.MappingExtensions;
using Module_02.Task_02.BLL.Models;
using Module_02.Task_02.DAL.Context;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;

public sealed class GetAllCategoriesQueryHandler : BaseCategoryQueryHandler, IRequestHandler<GetAllCategoriesQuery, Category.ItemModel[]>
{
    public GetAllCategoriesQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<Category.ItemModel[]> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await CategoriesDbSet.Select(entity => entity.ToCategoryItemModel()).ToArrayAsync(cancellationToken);
        return categories;
    }
}
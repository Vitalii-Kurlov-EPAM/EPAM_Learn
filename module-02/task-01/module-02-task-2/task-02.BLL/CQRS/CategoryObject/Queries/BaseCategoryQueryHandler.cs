using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;

public abstract class BaseCategoryQueryHandler : BaseHandler<IStudyReadOnlyDbContext>
{
    protected DbSet<CategoryEntity> CategoriesDbSet => DbContext.Categories;

    protected BaseCategoryQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }
}
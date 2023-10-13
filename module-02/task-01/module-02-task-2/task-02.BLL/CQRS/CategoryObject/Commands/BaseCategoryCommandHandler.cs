using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;

public abstract class BaseCategoryCommandHandler : BaseHandler<IWithTrackingStudyDbContext>
{
    protected DbSet<CategoryEntity> CategoriesDbSet => DbContext.Categories;

    protected BaseCategoryCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }
}
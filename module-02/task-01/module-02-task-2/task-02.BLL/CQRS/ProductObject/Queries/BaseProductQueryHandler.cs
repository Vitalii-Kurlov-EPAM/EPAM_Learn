using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Queries;

public abstract class BaseProductQueryHandler : BaseHandler<IStudyReadOnlyDbContext>
{
    protected DbSet<ProductEntity> ProductsDbSet => DbContext.Products;

    protected BaseProductQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Commands;

public abstract class BaseProductCommandHandler : BaseHandler<IWithTrackingStudyDbContext>
{
    protected DbSet<ProductEntity> ProductsDbSet => DbContext.Products;

    protected BaseProductCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }
}
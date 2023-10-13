using MediatR;
using Module_02.Task_01.CartingService.WebApi.DAL.Abstractions;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;
using Module_02.Task_01.CartingService.WebApi.DAL.Entities;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Commands;

public abstract class BaseImageCommandHandler : BaseHandler<IDbContext>
{
    protected IDbSet<ImageEntity> ImageDbSet => DbContext.Images;

    protected BaseImageCommandHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }
}
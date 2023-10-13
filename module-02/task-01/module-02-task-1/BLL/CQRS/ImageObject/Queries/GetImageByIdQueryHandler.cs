using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Queries;

public sealed class GetImageByIdQueryHandler : BaseImageQueryHandler,
    IRequestHandler<GetImageByIdQuery, Image.ItemModel>
{
    public GetImageByIdQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<Image.ItemModel> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
    {
        var imageEntity = ImageDbSet.GetById(request.ImageId);

        return Task.FromResult(imageEntity.ToImageItemModel());
    }
}
using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject.Queries;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.ImageObject.QueryHandlers;

public sealed class GetImageByIdQueryHandler : BaseQueryHandler,
    IRequestHandler<GetImageByIdQuery, ImageObjectModels.ItemModel>
{
    public GetImageByIdQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<ImageObjectModels.ItemModel> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
    {
        var imageEntity = DbContext.ImageRepository.GetById(request.ImageId);

        return Task.FromResult(imageEntity.ToImageItemModel());
    }
}
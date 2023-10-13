using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Commands;

public sealed class CreateImageCommandHandler : BaseImageCommandHandler,
    IRequestHandler<CreateImageCommand, int>
{
    public CreateImageCommandHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public Task<int> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var newImageEntity = request.ToImageEntity();

        ImageDbSet.Insert(newImageEntity);

        return Task.FromResult(newImageEntity.ImageId);
    }
}
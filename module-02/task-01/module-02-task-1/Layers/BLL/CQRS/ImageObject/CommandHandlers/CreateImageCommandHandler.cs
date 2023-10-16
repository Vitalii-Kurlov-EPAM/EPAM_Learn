using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.ImageObject.CommandHandlers;

public sealed class CreateImageCommandHandler : BaseCommandHandler,
    IRequestHandler<CreateImageCommand, int>
{
    public CreateImageCommandHandler(IDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<int> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var newImageEntity = request.ToImageEntity();

        DbContext.ImageRepository.Add(newImageEntity);

        return Task.FromResult(newImageEntity.ImageId);
    }
}
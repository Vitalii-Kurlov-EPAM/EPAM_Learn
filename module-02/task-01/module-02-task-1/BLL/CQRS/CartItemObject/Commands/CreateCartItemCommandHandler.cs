using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.MappingExtensions;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;
using Module_02.Task_01.CartingService.WebApi.DAL.Entities;
using Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Commands;
using Сommon.DB.Exceptions;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Commands;

public sealed class CreateCartItemCommandHandler : BaseCartItemCommandHandler,
    IRequestHandler<CreateCartItemCommand, int>
{
    public CreateCartItemCommandHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    private async Task<ImageEntity> CreateImage(CreateImageCommand command, CancellationToken cancellationToken)
    {
        var imageId = await Mediator.Send(command, cancellationToken);

        var newImage = ImagesDbSet.GetById(imageId);

        return newImage ??
               throw new DbRecordNotFoundException(
                   $"Something went wrong, image record with ID={imageId} not found.");
    }

    private async Task<int> UpdateCartItemEntity(CartItemEntity entity, CreateCartItemCommand command,
        CancellationToken cancellationToken)
    {
        entity.Name = command.Name;
        entity.Price = command.Price;
        entity.Quantity += command.Quantity;

        if (string.IsNullOrEmpty(command.ImageUrl))
        {
            if (entity.Image != null)
            {
                ImagesDbSet.Delete(entity.Image);
                entity.Image = null;
            }
        }
        else
        {
            if (entity.Image != null)
            {
                var image = ImagesDbSet.GetById(entity.Image.ImageId);
                image.Url = command.ImageUrl;
                image.Alt = command.ImageAlt;
                ImagesDbSet.Update(image);
            }
            else
            {
                entity.Image = await CreateImage(command.ToCreateImageCommand(), cancellationToken);
            }
        }

        CartItemsDbSet.Update(entity);

        return entity.CartItemId;
    }

    private async Task<int> CreateCartItemEntity(CreateCartItemCommand command, CancellationToken cancellationToken)
    {
        ImageEntity newImage = null;

        if (!string.IsNullOrWhiteSpace(command.ImageUrl))
        {
            newImage = await CreateImage(command.ToCreateImageCommand(), cancellationToken);
        }

        var newCartItemEntity = command.ToCartItemEntity(newImage);
        CartItemsDbSet.Insert(newCartItemEntity);

        return newCartItemEntity.CartItemId;
    }

    public async Task<int> Handle(CreateCartItemCommand command, CancellationToken cancellationToken)
    {
        var foundCartItem = CartItemsDbSet.Include(entity => entity.Image)
            .Where(entity => entity.CartId == command.CartId && entity.Id == command.Id)
            .FirstOrDefault();

        DbContext.BeginTransaction();

        var cartItemId = foundCartItem == null
            ? await CreateCartItemEntity(command, cancellationToken)
            : await UpdateCartItemEntity(foundCartItem, command, cancellationToken);

        DbContext.CommitChanges();

        return cartItemId;
    }
}
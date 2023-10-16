using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.CartItemObject.CommandHandlers;

public sealed class CreateCartItemCommandHandler : BaseCommandHandler,
    IRequestHandler<CreateCartItemCommand, CartItemObjectModels.ItemModel>
{
    public CreateCartItemCommandHandler(IDbContext dbContext)
        : base(dbContext)
    {
    }

    private ImageEntity CreateImage(CreateCartItemCommand command)
    {
        var image = new ImageEntity
        {
            ImageId = 0,
            Url = command.ImageUrl,
            Alt = command.ImageAlt
        };

        DbContext.ImageRepository.Add(image);
        return image;
    }

    private CartItemObjectModels.ItemModel UpdateCartItemEntity(CartItemEntity entity, CreateCartItemCommand command)
    {
        entity.Name = command.Name;
        entity.Price = command.Price;
        entity.Quantity += command.Quantity;

        if (string.IsNullOrEmpty(command.ImageUrl))
        {
            if (entity.Image != null)
            {
                DbContext.ImageRepository.Delete(entity.Image);
                entity.Image = null;
            }
        }
        else
        {
            if (entity.Image != null)
            {
                var image = DbContext.ImageRepository.GetById(entity.Image.ImageId);
                image.Url = command.ImageUrl;
                image.Alt = command.ImageAlt;
                DbContext.ImageRepository.Update(image);
            }
            else
            {
                entity.Image = CreateImage(command);
            }
        }

        DbContext.CartItemRepository.Update(entity);

        return entity.ToCartItemModel();
    }

    private CartItemObjectModels.ItemModel CreateCartItemEntity(CreateCartItemCommand command)
    {
        ImageEntity newImage = null;

        if (!string.IsNullOrWhiteSpace(command.ImageUrl))
        {
            newImage = CreateImage(command);
        }

        var newCartItemEntity = command.ToCartItemEntity(newImage);
        DbContext.CartItemRepository.Add(newCartItemEntity);

        return newCartItemEntity.ToCartItemModel();
    }

    public Task<CartItemObjectModels.ItemModel> Handle(CreateCartItemCommand command, CancellationToken cancellationToken)
    {
        var foundCartItem = DbContext.CartItemRepository.GetById(command.Id, true);

        DbContext.BeginTransaction();

        var cartItem = foundCartItem == null
            ? CreateCartItemEntity(command)
            : UpdateCartItemEntity(foundCartItem, command);

        DbContext.CommitChanges();

        return Task.FromResult(cartItem);
    }
}
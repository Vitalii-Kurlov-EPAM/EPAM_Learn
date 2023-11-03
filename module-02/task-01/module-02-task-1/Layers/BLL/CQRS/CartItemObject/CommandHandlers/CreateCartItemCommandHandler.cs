using MediatR;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;
using Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.Base;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Utils;

namespace Module_02.Task_01.CartingService.WebApi.Layers.BLL.CQRS.CartItemObject.CommandHandlers;

public sealed class CreateCartItemCommandHandler : BaseCommandHandler,
    IRequestHandler<CreateCartItemCommand, CartItemObjectModels.ItemModel>
{
    public CreateCartItemCommandHandler(IDbContext dbContext)
        : base(dbContext)
    {
    }

    private CartItemObjectModels.ItemModel UpdateCartItemEntity(CartItemEntity entity, CreateCartItemCommand command)
    {
        entity.Name = command.Name;
        entity.Price = command.Price;
        entity.Quantity += command.Quantity;


        DbContext.CreateOrUpdateImage(entity, command.ImageUrl, command.ImageAlt);

        DbContext.CartItemRepository.Update(entity);

        return entity.ToCartItemModel();
    }

    private CartItemObjectModels.ItemModel CreateCartItemEntity(CreateCartItemCommand command)
    {
        ImageEntity newImage = null;

        if (!string.IsNullOrWhiteSpace(command.ImageUrl))
        {
            newImage = DbContext.CreateImage(command.ImageUrl, command.ImageAlt);
        }

        var newCartItemEntity = command.ToCartItemEntity(newImage);
        DbContext.CartItemRepository.Add(newCartItemEntity);

        return newCartItemEntity.ToCartItemModel();
    }

    public Task<CartItemObjectModels.ItemModel> Handle(CreateCartItemCommand command, CancellationToken cancellationToken)
    {
        var foundCartItem = DbContext.CartItemRepository.GetCartItem(command.CartId, command.Id, true);

        DbContext.BeginTransaction();

        var cartItem = foundCartItem == null
            ? CreateCartItemEntity(command)
            : UpdateCartItemEntity(foundCartItem, command);

        DbContext.CommitChanges();

        return Task.FromResult(cartItem);
    }
}
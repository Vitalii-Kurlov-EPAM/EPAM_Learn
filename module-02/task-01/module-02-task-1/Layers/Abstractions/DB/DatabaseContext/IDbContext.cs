using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.DatabaseContext;

public interface IDbContext
{
    ICartItemRepository CartItemRepository { get; }

    IImageRepository ImageRepository { get; }
}
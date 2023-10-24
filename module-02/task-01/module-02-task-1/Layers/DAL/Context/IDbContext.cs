using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

public interface IDbContext : IDbTransactionMethods, IDbSetContext
{
    ICartItemRepository CartItemRepository { get; }
    IImageRepository ImageRepository { get; }
}
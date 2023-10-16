using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

public interface IDbSetContext
{
    IDbSet<CartItemEntity> CartItems { get; }
    IDbSet<ImageEntity> Images { get;  }
}
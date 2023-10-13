using Module_02.Task_01.CartingService.WebApi.DAL.Abstractions;
using Module_02.Task_01.CartingService.WebApi.DAL.Entities;

namespace Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

public interface IDbContext : IDbTransactionMethods
{
    Abstractions.IDbSet<CartItemEntity> CartItems { get; }
    Abstractions.IDbSet<ImageEntity> Images { get; }
}
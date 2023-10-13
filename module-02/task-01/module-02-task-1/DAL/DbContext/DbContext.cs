using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using LiteDB;
using Module_02.Task_01.CartingService.WebApi.DAL.Abstractions;
using Module_02.Task_01.CartingService.WebApi.DAL.DbEngine.LiteDb;
using Module_02.Task_01.CartingService.WebApi.DAL.Entities;
using Сommon.DB.Abstractions;

namespace Module_02.Task_01.CartingService.WebApi.DAL.DbContext;

public class DbContext : LiteDbEngine, IDbContext
{
    public DbContext(IDbConnectionSettings dbConnectionSettings)
    : base(dbConnectionSettings)
    {
    }

    private IDbSet<CartItemEntity> _cartItems;
    public IDbSet<CartItemEntity> CartItems => _cartItems ??= GetDbSet<CartItemEntity>();

    private IDbSet<ImageEntity> _images;
    public IDbSet<ImageEntity> Images => _images ??= GetDbSet<ImageEntity>();


    static DbContext()
    {
        var mapper = BsonMapper.Global;

        mapper.Entity<CartItemEntity>()
            .Id(x => x.CartItemId)
            .Field(x => x.CartId, "cart_id")
            .Field(x => x.Id, "id")
            .Field(x => x.Name, "name")
            .Field(x => x.Image, "image")
            .Field(x => x.Price, "price")
            .Field(x => x.Quantity, "quantity")
            .DbRef(x => x.Image, typeof(ImageEntity).GetCustomAttribute<TableAttribute>()!.Name);

        mapper.Entity<ImageEntity>()
            .Id(x => x.ImageId)
            .Field(x => x.Url, "url")
            .Field(x => x.Alt, "alt");
    }
}
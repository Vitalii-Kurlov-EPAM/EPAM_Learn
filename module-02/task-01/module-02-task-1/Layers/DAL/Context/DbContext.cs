using LiteDB;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Consts;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.DbEngine.LiteDb;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Repositories;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

public class DbContext : LiteDbEngine, IDbContext
{
    public DbContext(IDbConnectionSettings dbConnectionSettings)
    : base(dbConnectionSettings)
    {
        CartItemRepository = new CartItemRepository(this);
        ImageRepository = new ImageRepository(this);
    }

    private IDbSet<CartItemEntity> _cartItems;
    public IDbSet<CartItemEntity> CartItems => _cartItems ??= GetDbSet<CartItemEntity>(SourceCollectionNames.CART_ITEMS_COLLECTION_NAME);

    private IDbSet<ImageEntity> _images;
    public IDbSet<ImageEntity> Images => _images ??= GetDbSet<ImageEntity>(SourceCollectionNames.IMAGES_COLLECTION_NAME);

    public ICartItemRepository CartItemRepository { get; }
    public IImageRepository ImageRepository { get; }

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
            .DbRef(x => x.Image, SourceCollectionNames.IMAGES_COLLECTION_NAME);

        mapper.Entity<ImageEntity>()
            .Id(x => x.ImageId)
            .Field(x => x.Url, "url")
            .Field(x => x.Alt, "alt");
    }
}
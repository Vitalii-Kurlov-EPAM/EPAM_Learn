using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Repositories;

public class CartItemRepository : BaseRepository, ICartItemRepository
{
    private IDbSet<CartItemEntity> DbSet => DbContext.CartItems;

    public CartItemRepository(IDbSetContext dbContext)
        : base(dbContext)
    {
    }

    public IEnumerable<CartItemEntity> GetAll(bool includeDeps = false)
    {
        var itemsDbSet = includeDeps
            ? DbSet.Include(x => x.Image)
            : DbSet;

        return itemsDbSet.All();
    }

    public CartItemEntity GetById(int id, bool includeDeps = false)
    {
        var itemsDbSet = includeDeps
            ? DbSet.Include(x => x.Image)
            : DbSet;

        return itemsDbSet.GetById(id);
    }

    public bool DeleteById(int id)
    {
        return DbSet.Delete(id);
    }

    public bool Delete(CartItemEntity entity)
    {
        return DbSet.Delete(entity);
    }

    public int Add(CartItemEntity entity)
    {
        return Convert.ToInt32(DbSet.Insert(entity));
    }

    public bool Update(CartItemEntity entity)
    {
        return DbSet.Update(entity);
    }

    public bool IsExistById(int id)
    {
        return DbSet.Any(entity => entity.CartItemId == id);
    }

    public IEnumerable<CartItemEntity> GetAllCartItems(string cartId, bool includeDeps = false)
    {
        var itemsDbSet = includeDeps
            ? DbSet.Include(x => x.Image)
            : DbSet;

        return itemsDbSet.Where(entity => entity.CartId == cartId);
    }

    public CartItemEntity GetCartItem(string cartId, int cartItemId, bool includeDeps = false)
    {
        var itemsDbSet = includeDeps
            ? DbSet.Include(x => x.Image)
            : DbSet;

        return itemsDbSet.Where(entity => entity.CartItemId == cartItemId && entity.CartId == cartId).FirstOrDefault();
    }
}
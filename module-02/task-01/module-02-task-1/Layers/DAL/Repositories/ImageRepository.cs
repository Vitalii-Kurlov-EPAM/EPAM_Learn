using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Repositories;

public class ImageRepository : BaseRepository, IImageRepository
{
    private IDbSet<ImageEntity> DbSet => DbContext.Images;

    public ImageRepository(IDbSetContext dbContext)
        : base(dbContext)
    {
    }

    public IEnumerable<ImageEntity> GetAll(bool includeDeps = false)
    {
        return DbSet.All();
    }

    public ImageEntity GetById(int id, bool includeDeps = false)
    {
        return DbSet.GetById(id);
    }

    public bool DeleteById(int id)
    {
        return DbSet.Delete(id);
    }

    public bool Delete(ImageEntity entity)
    {
        return DbSet.Delete(entity);
    }

    public int Add(ImageEntity entity)
    {
        return Convert.ToInt32(DbSet.Insert(entity));
    }

    public bool Update(ImageEntity entity)
    {
        return DbSet.Update(entity);
    }

    public bool IsExistById(int id)
    {
        return DbSet.Any(entity => entity.ImageId == id);
    }
}
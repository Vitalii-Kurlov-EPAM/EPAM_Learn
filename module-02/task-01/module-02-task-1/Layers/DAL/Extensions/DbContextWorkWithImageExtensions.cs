using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Utils;

public static class DbContextWorkWithImageExtensions
{

    public static ImageEntity CreateImage(this IDbContext dbContext, string imageUrl, string imageAlt)
    {
        var image = new ImageEntity
        {
            ImageId = 0,
            Url = imageUrl,
            Alt = imageAlt ?? string.Empty
        };

        dbContext.ImageRepository.Add(image);
        return image;
    }

    public static void CreateOrUpdateImage(this IDbContext dbContext, CartItemEntity entity, string imageUrl, string imageAlt)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            if (entity.Image != null)
            {
                dbContext.ImageRepository.Delete(entity.Image);
                entity.Image = null;
            }
        }
        else
        {
            if (entity.Image != null)
            {
                var image = dbContext.ImageRepository.GetById(entity.Image.ImageId);
                image.Url = imageUrl;
                if (imageAlt != null)
                {
                    image.Alt = imageAlt;
                }
                dbContext.ImageRepository.Update(image);
            }
            else
            {
                entity.Image = CreateImage(dbContext, imageUrl, imageAlt);
            }
        }
    }
}
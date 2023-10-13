using Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Commands;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;
using Module_02.Task_01.CartingService.WebApi.DAL.Entities;

namespace Module_02.Task_01.CartingService.WebApi.BLL.MappingExtensions;

internal static class ImageModelExtensions
{
    public static ImageEntity ToImageEntity(this CreateImageCommand value)
        => new()
        {
            ImageId = 0,
            Alt = value.Alt,
            Url = value.Url
        };


    public static Image.ItemModel ToImageItemModel(this ImageEntity value)
        => value == null ? null : new Image.ItemModel
        {
            ImageId = value.ImageId,
            ImageAlt = value.Alt,
            ImageUrl = value.Url
        };
}
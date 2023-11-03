using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject.Commands;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;


namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.Extensions.ModelMapping;

internal static class ImageModelExtensions
{
    public static ImageEntity ToImageEntity(this CreateImageCommand value)
        => new()
        {
            ImageId = 0,
            Alt = value.Alt,
            Url = value.Url
        };


    public static ImageObjectModels.ItemModel ToImageItemModel(this ImageEntity value)
        => value == null ? null : new ImageObjectModels.ItemModel
        {
            ImageId = value.ImageId,
            ImageAlt = value.Alt,
            ImageUrl = value.Url
        };
}
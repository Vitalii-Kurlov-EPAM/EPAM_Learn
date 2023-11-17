namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject;

public static class ImageObjectModels
{
    public class ItemModel
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
    }
}
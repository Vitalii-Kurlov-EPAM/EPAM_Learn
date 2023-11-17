using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject;

public static class CartItemObjectModels
{
    public class ItemModel
    {
        public int CartItemId { get; set; }

        public string CartId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public ImageObjectModels.ItemModel Image { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
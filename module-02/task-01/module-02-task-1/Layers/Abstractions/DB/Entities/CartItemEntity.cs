namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities
{
    public class CartItemEntity
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public ImageEntity Image { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

namespace Module_02.Task_01.CartingService.WebApi.BLL.Models;

public static class CartItem
{
    public class ItemModel
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public Image.ItemModel Image { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
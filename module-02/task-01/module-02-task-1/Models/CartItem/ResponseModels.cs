namespace Module_02.Task_01.CartingService.WebApi.Models.CartItem;

public static class CartItemResponse
{
    public class ItemDto
    {
        #region Item's data

        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        #endregion

        #region Image's data

        public int? ImageId { get; set; }

        public string ImageUrl { get; set; }

        public string ImageAlt { get; set; }

        #endregion
    }
}


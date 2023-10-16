namespace Module_02.Task_01.CartingService.WebApi.Models.CartItem;


public static class CartItemRequest
{
    public class CreateModel
    {
        #region Item's data

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        #endregion

        #region Image's data

        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }

        #endregion
    }
}
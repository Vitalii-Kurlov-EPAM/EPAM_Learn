using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_01.CartingService.WebApi.Models.CartItem;

public static class CartItemResponse
{
    public class ItemDto
    {
        #region Item's data

        [Required]
        public int CartItemId { get; set; }

        [Required] 
        public string CartId { get; set; }

        [Required] 
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required] 
        public decimal Price { get; set; }

        [Required] 
        public int Quantity { get; set; }

        #endregion

        #region Image's data

        public int? ImageId { get; set; }

        public string ImageUrl { get; set; }

        public string ImageAlt { get; set; }

        #endregion
    }

    public class CartItemsDto
    {
        public string CartId { get; set; }
        public ItemDto[] Items { get; set;}
    }
}


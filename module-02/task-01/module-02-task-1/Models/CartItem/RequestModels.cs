using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_01.CartingService.WebApi.Models.CartItem;


public static class CartItemRequest
{
    public class CreateModel
    {
        #region Item's data

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

        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }

        #endregion
    }
}
using System.ComponentModel.DataAnnotations;
using Module_02.Task_02.CatalogService.WebApi.Models.Category;

namespace Module_02.Task_02.CatalogService.WebApi.Models.Product;

public static class ProductResponse
{
    public sealed class ItemDto
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Image { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int Amount { get; set; }

        [Required]
        public CategoryResponse.ItemDto CategoryItem { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_02.CatalogService.WebApi.Models.Product;

public static class ProductRequest
{
    public sealed class CreateModel
    {
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
    }

    public sealed class UpdateModel
    {
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
    }
}
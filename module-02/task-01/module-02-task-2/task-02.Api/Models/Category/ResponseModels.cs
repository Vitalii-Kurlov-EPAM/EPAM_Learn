using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_02.CatalogService.WebApi.Models.Category;

public static class CategoryResponse
{
    public sealed class ItemDto
    {
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Image { get; set; }
        
        public int? ParentCategoryId { get; set; }

        public ItemDto ParentCategory { get; set; }
    }
}

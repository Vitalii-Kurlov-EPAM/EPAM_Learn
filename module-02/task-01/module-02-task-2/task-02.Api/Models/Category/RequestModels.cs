using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_02.CatalogService.WebApi.Models.Category;

public static class CategoryRequest
{
    public sealed class CreateModel 
    {
        [Required]
        public string Name { get; set; }

        public string Image { get; set; }
        
        public int? ParentCategoryId { get; set; }
    }

    public sealed class UpdateModel
    {
        [Required]
        public string Name { get; set; }

        public string Image { get; set; }
        
        public int? ParentCategoryId { get; set; }

    }
}
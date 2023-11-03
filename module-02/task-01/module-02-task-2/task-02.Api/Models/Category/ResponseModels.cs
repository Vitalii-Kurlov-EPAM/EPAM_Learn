using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_02.CatalogService.WebApi.Models.Category;

public static class CategoryResponse
{
    public sealed class ItemDto
    {
        [Required]
        public int CategoryId { get; init; }
        
        [Required]
        public string Name { get; init; }
        
        public string Image { get; init; }
        
        public int? ParentCategoryId { get; init; }

        public ItemDto ParentCategory { get; init; }
    }

    public sealed class CreatedDto
    {
        [Required]
        public ItemDto Result { get; }

        [Required]
        public ResourceLink[] Links { get; }

        public CreatedDto(ItemDto result, ResourceLink[] links)
        {
            Result = result;
            Links = links;
        }
    }
}
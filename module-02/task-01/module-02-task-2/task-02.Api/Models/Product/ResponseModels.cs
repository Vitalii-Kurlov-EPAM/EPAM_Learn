using System.ComponentModel.DataAnnotations;
using Module_02.Task_02.CatalogService.Abstractions.DB;
using Module_02.Task_02.CatalogService.WebApi.Models.Category;

namespace Module_02.Task_02.CatalogService.WebApi.Models.Product;

public static class ProductResponse
{
    public sealed class ItemDto
    {
        [Required] public int ProductId { get; init; }

        [Required] public string Name { get; init; }

        public string Description { get; init; }

        public string Image { get; init; }

        [Required] public int CategoryId { get; init; }

        [Required] public decimal Price { get; init; }

        [Required] public int Amount { get; init; }

        [Required] public CategoryResponse.ItemDto CategoryItem { get; init; }
    }

    public sealed class CreatedDto
    {
        [Required] public ItemDto Result { get; }

        [Required] public ResourceLink[] Links { get; }

        public CreatedDto(ItemDto result, ResourceLink[] links)
        {
            Result = result;
            Links = links;
        }
    }

    public sealed class PagedDto : PagedResult<ItemDto>
    {
        [Required]
        public override int CurrentPage { get; set; }
        
        [Required]
        public override int PageCount { get; set; }
        
        [Required]
        public override int PageSize { get; set; }

        [Required]
        public override int RowCount { get; set; }

        [Required]
        public override int FirstRowOnPage => base.FirstRowOnPage;

        [Required]
        public override int LastRowOnPage => base.LastRowOnPage;

        [Required]
        public override ItemDto[] Records { get; set; }
    }
}
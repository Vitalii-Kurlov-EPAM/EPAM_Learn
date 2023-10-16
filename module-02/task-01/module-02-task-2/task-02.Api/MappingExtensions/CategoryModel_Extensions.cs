using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;
using Module_02.Task_02.CatalogService.WebApi.Models.Category;

namespace Module_02.Task_02.CatalogService.WebApi.MappingExtensions;

internal static class CategoryModelExtensions
{
    public static CategoryResponse.ItemDto ToCategoryResponseItemDto(this CategoryObjectModels.ItemModel value)
        => value == null
            ? null
            : new CategoryResponse.ItemDto
            {
                CategoryId = value.CategoryId,
                Name = value.Name,
                Image = value.Image,
                ParentCategoryId = value.ParentCategoryId,
                ParentCategory = value.ParentCategory == null
                    ? null
                    : new CategoryResponse.ItemDto
                    {
                        CategoryId = value.ParentCategory.CategoryId,
                        Name = value.ParentCategory.Name,
                        Image = value.ParentCategory.Image,
                        ParentCategoryId = value.ParentCategory.ParentCategoryId,
                        ParentCategory = null
                    }
            };

    public static CreateCategoryCommand ToCreateCategoryCommand(this CategoryRequest.CreateModel value)
        => new()
        {
            Name = value.Name,
            Image = value.Image,
            ParentCategoryId = value.ParentCategoryId
        };

    public static UpdateCategoryCommand ToUpdateCategoryCommand(this CategoryRequest.UpdateModel value, int categoryId)
        => new()
        {
            CategoryId = categoryId,
            Name = value.Name,
            Image = value.Image,
            ParentCategoryId = value.ParentCategoryId            
        };
}
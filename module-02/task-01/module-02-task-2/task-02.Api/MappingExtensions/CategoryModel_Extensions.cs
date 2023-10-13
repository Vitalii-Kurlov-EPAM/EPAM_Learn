using Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;
using Module_02.Task_02.BLL.Models;
using Module_02.Task_02.CatalogService.WebApi.Models.Category;

namespace Module_02.Task_02.CatalogService.WebApi.MappingExtensions;

internal static class CategoryModelExtensions
{
    public static CategoryResponse.ItemDto ToCategoryResponseItemDto(this Category.ItemModel value)
        => value == null
            ? null
            : new CategoryResponse.ItemDto
            {
                CategoryId = value.CategoryId,
                Name = value.Name,
                Image = value.Image,
                ParentCategoryId = value.ParentCategoryId
            };

    public static CreateCategoryCommand ToCreateCategoryCommand(this CategoryRequest.CreateModel value)
        => new()
        {
            Name = value.Name,
            Image = value.Image,
            ParentCategoryId = value.ParentCategoryId
        };

    public static UpdateCategoryCommand ToUpdateCategoryCommand(this CategoryRequest.UpdateModel value, int categoryId)
        => new(categoryId)
        {
            Name = value.Name,
            Image = value.Image,
            ParentCategoryId = value.ParentCategoryId            
        };
}
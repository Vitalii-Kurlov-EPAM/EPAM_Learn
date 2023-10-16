using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;

namespace Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;

public static class CategoryModelExtensions
{
    public static CategoryObjectModels.ItemModel ToCategoryItemModel(this CategoryEntity value)
        => value == null
            ? null
            : new CategoryObjectModels.ItemModel
            {
                CategoryId = value.CategoryId,
                Name = value.Name,
                Image = value.Image,
                ParentCategoryId = value.ParentCategoryId,
                ParentCategory = value.ParentCategory == null
                    ? null
                    : new CategoryObjectModels.ItemModel
                    {
                        CategoryId = value.ParentCategory.CategoryId,
                        Name = value.ParentCategory.Name,
                        Image = value.ParentCategory.Image,
                        ParentCategoryId = value.ParentCategory.ParentCategoryId,
                        ParentCategory = null
                    }
            };


    public static CategoryEntity ToCategoryEntity(this CreateCategoryCommand value)
        => value == null
            ? null
            : new CategoryEntity
            {
                CategoryId = 0,
                Name = value.Name,
                Image = value.Image,
                ParentCategoryId = value.ParentCategoryId,
                ParentCategory = null
            };
}
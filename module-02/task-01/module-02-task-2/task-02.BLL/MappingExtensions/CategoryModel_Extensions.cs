using Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;
using Module_02.Task_02.BLL.Models;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.MappingExtensions;

internal static class CategoryModelExtensions
{
    public static Category.ItemModel ToCategoryItemModel(this CategoryEntity value)
        => value == null
            ? null
            : new Category.ItemModel
            {
                CategoryId = value.CategoryId,
                Name = value.Name,
                Image = value.Image,
                ParentCategoryId = value.ParentCategoryId
            };

    public static CategoryEntity ToCategoryEntity(this CreateCategoryCommand value)
        => value == null
            ? null
            : new CategoryEntity
            {
                CategoryId = 0,
                Name = value.Name,
                Image = value.Image,
                ParentCategoryId = value.ParentCategoryId
            };

    public static CategoryEntity ToCategoryEntity(this UpdateCategoryCommand value)
        => value == null
            ? null
            : new CategoryEntity
            {
                CategoryId = value.CategoryId,
                Name = value.Name,
                Image = value.Image,
                ParentCategoryId = value.ParentCategoryId
            };
}
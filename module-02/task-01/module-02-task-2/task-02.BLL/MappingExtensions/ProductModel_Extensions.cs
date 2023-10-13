using Module_02.Task_02.BLL.CQRS.ProductObject.Commands;
using Module_02.Task_02.BLL.Models;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.MappingExtensions;

internal static class ProductModelExtensions
{
    public static Product.ItemModel ToProductItemModel(this ProductEntity value)
        => value == null
            ? null
            : new Product.ItemModel
            {
                ProductId = value.ProductId,
                CategoryId = value.CategoryId,
                Name = value.Name,
                Description = value.Description,
                Image = value.Image,
                Price = value.Price,
                Amount = value.Amount,
                Category = value.Category.ToCategoryItemModel()
            };

    public static ProductEntity ToProductEntity(this CreateProductCommand value)
        => value == null
            ? null
            : new ProductEntity
            {
                ProductId = 0,
                CategoryId = value.CategoryId,
                Name = value.Name,
                Description = value.Description,
                Image = value.Image,
                Price = value.Price,
                Amount = value.Amount
            };

    public static ProductEntity ToProductEntity(this UpdateProductCommand value)
        => value == null
            ? null
            : new ProductEntity
            {
                ProductId = value.ProductId,
                CategoryId = value.CategoryId,
                Name = value.Name,
                Description = value.Description,
                Image = value.Image,
                Price = value.Price,
                Amount = value.Amount
            };
}
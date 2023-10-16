using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;

namespace Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;

public static class ProductModelExtensions
{
    public static ProductObjectModels.ItemModel ToProductItemModel(this ProductEntity value)
        => value == null
            ? null
            : new ProductObjectModels.ItemModel
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
}
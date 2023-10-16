using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;
using Module_02.Task_02.CatalogService.WebApi.Models.Product;

namespace Module_02.Task_02.CatalogService.WebApi.MappingExtensions;

internal static class ProductModelExtensions
{
    public static ProductResponse.ItemDto ToProductResponseItemDto(this ProductObjectModels.ItemModel value)
        => value == null
            ? null
            : new ProductResponse.ItemDto
            {
                ProductId = value.ProductId,
                Name = value.Name,
                Description = value.Description,
                Image = value.Image,
                CategoryId = value.CategoryId,
                Price = value.Price,
                Amount = value.Amount,
                CategoryItem = value.Category.ToCategoryResponseItemDto(),
            };

    public static CreateProductCommand ToCreateProductCommand(this ProductRequest.CreateModel value)
        => new()
        {
            Name = value.Name,
            Description = value.Description,
            Image = value.Image,
            CategoryId = value.CategoryId,
            Price = value.Price,
            Amount = value.Amount
        };

    public static UpdateProductCommand ToUpdateProductCommand(this ProductRequest.UpdateModel value, int productId)
        => new()
        {
            ProductId = productId,
            Name = value.Name,
            Description = value.Description,
            Image = value.Image,
            CategoryId = value.CategoryId,
            Price = value.Price,
            Amount = value.Amount
        };
}
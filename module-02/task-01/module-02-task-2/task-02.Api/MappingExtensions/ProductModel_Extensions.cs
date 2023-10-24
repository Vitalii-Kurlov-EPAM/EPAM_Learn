using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject.Commands;
using Module_02.Task_02.CatalogService.Abstractions.DB;
using Module_02.Task_02.CatalogService.WebApi.Models;
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

    public static ProductResponse.PagedDto ToPagedProductResponseItemDto(this PagedResult<ProductObjectModels.ItemModel> value)
        => value == null
            ? null
            : new ProductResponse.PagedDto
            {
                PageSize = value.PageSize,
                CurrentPage = value.CurrentPage,
                PageCount = value.PageCount,
                RowCount = value.RowCount,
                Records = value.Records.Select(entity => entity.ToProductResponseItemDto()).ToArray()
            };

    public static ProductResponse.CreatedDto ToProductResponseCreatedDto(this ProductObjectModels.ItemModel value, params ResourceLink[] links)
        => value == null
            ? null
            : new ProductResponse.CreatedDto(value.ToProductResponseItemDto(), links);


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
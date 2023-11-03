using System.Diagnostics.CodeAnalysis;
using MessageModels.Entity;
using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;

namespace Module_02.Task_02.CatalogService.BLL.ModelExtensions;

public static class ProductEntityExtensions
{
    public static ProductMessage ToProductMessage([NotNull] this ProductEntity value)
        => new(
            value.ProductId,
            value.Name,
            value.Description,
            value.Image,
            value.CategoryId,
            value.Price,
            value.Amount
        );


}
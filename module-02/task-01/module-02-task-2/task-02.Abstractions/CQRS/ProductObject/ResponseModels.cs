using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject;

namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.ProductObject;

public static class ProductObjectModels
{
    public sealed class ItemModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public CategoryObjectModels.ItemModel Category { get; set; }
    }
}
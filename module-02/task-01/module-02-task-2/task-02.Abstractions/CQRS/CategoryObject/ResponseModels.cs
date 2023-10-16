namespace Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject;

public static class CategoryObjectModels
{
    public sealed class ItemModel
    {
        public required int CategoryId { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }
        public required int? ParentCategoryId { get; set; }
        public required ItemModel ParentCategory { get; set; }
    }
}
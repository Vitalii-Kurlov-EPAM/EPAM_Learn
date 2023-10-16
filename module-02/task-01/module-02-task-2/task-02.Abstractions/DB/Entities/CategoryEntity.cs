namespace Module_02.Task_02.CatalogService.Abstractions.DB.Entities;

public class CategoryEntity
{
    public int CategoryId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public int? ParentCategoryId { get; set; }

    public CategoryEntity ParentCategory { get; set; }
}
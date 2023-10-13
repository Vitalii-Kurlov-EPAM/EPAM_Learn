namespace Module_02.Task_02.DAL.Entities;

public sealed class CategoryEntity
{
    public int CategoryId { get; set; }
    
    public string Name{ get; set; }

    public string Image { get; set; }

    public int? ParentCategoryId { get; set; }
    
    public CategoryEntity ParentCategory { get; set; }
}

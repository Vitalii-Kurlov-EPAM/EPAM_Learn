namespace Module_02.Task_02.DAL.Entities;

public sealed class ProductEntity
{
    public int ProductId { get; set; }
    
    public string Name{ get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }

    public CategoryEntity Category { get; set; }
}

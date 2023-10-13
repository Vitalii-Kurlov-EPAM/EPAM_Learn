namespace Module_02.Task_02.BLL.Models;

public static class Product
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
        public Category.ItemModel Category { get; set; }
    }
}
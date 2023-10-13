namespace Module_02.Task_02.BLL.Models;

public static class Category
{
    public sealed class ItemModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
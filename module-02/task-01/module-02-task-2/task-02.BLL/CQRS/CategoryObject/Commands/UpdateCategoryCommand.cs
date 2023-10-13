using MediatR;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;

public sealed class UpdateCategoryCommand : IRequest<bool>
{
    public int CategoryId { get; }

    public string Name { get; set; }

    public string Image { get; set; }

    public int? ParentCategoryId { get; set; }

    public UpdateCategoryCommand(int categoryId)
    {
        CategoryId = categoryId;
    }

}
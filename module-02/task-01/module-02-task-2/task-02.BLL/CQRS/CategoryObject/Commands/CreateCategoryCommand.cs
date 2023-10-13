using MediatR;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;

public sealed class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; set; }

    public string Image { get; set; }

    public int? ParentCategoryId { get; set; }
}
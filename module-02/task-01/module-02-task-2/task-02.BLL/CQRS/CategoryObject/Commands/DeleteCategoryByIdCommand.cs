using MediatR;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Commands;

public sealed class DeleteCategoryByIdCommand : IRequest<bool>
{
    public int CategoryId { get; }

    public DeleteCategoryByIdCommand(int categoryId)
    {
        CategoryId = categoryId;
    }
}
using MediatR;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;

public sealed class CheckCategoryIsExistByIdQuery : IRequest<bool>
{
    public int CategoryId { get; }

    public CheckCategoryIsExistByIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}
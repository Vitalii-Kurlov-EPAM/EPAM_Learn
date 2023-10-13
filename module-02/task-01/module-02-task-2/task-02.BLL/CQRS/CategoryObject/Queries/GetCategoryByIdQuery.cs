using MediatR;
using Module_02.Task_02.BLL.Models;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;

public sealed class GetCategoryByIdQuery : IRequest<Category.ItemModel>
{
    public int CategoryId { get; }

    public GetCategoryByIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}
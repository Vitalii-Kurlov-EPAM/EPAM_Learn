using MediatR;
using Module_02.Task_02.BLL.Models;

namespace Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;

public sealed class GetAllCategoriesQuery : IRequest<Category.ItemModel[]>
{
}
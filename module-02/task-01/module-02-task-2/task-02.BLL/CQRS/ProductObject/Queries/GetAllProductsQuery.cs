using MediatR;
using Module_02.Task_02.BLL.Models;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Queries;

public sealed class GetAllProductsQuery : IRequest<Product.ItemModel[]>
{
    public bool IncludeDependencies { get; }

    public GetAllProductsQuery(bool includeDependencies)
    {
        IncludeDependencies = includeDependencies;
    }
}
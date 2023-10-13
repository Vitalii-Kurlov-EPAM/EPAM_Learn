using MediatR;
using Module_02.Task_02.BLL.Models;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Queries;

public sealed class GetProductByIdQuery : IRequest<Product.ItemModel>
{
    public int ProductId { get; }
    public bool IncludeDependencies { get; }
   
    public GetProductByIdQuery(int productId, bool includeDependencies)
    {
        ProductId = productId;
        IncludeDependencies = includeDependencies;
    }
}
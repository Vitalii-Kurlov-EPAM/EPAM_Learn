using MediatR;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Queries;

public sealed class CheckProductIsExistByIdQuery : IRequest<bool>
{
    public int ProductId { get; }

    public CheckProductIsExistByIdQuery(int productId)
    {
        ProductId = productId;
    }
}
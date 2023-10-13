using MediatR;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Commands;

public sealed class DeleteProductByIdCommand : IRequest<bool>
{
    public int ProductId { get; }
    public int Amount { get; }

    public DeleteProductByIdCommand(int productId, int amount)
    {
        ProductId = productId;
        Amount = amount;
    }
}
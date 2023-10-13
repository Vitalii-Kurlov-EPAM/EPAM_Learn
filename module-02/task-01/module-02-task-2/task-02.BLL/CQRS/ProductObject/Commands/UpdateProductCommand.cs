using MediatR;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Commands;

public sealed class UpdateProductCommand : IRequest<bool>
{
    public int ProductId { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }

    public UpdateProductCommand(int productId)
    {
        ProductId = productId;
    }

}
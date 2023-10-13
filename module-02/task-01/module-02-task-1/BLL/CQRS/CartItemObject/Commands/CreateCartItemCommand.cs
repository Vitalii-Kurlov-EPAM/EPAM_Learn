using MediatR;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Commands;

public sealed class CreateCartItemCommand : IRequest<int>
{
    #region Item's data

    public int CartId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    #endregion

    #region Image's data

    public string ImageUrl { get; set; }
    public string ImageAlt { get; set; }

    #endregion
}
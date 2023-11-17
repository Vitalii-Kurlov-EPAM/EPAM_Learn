using Common.MessageBrokerAbstractions.MessageHandlers;
using MediatR;
using MessageBroker.RabbitMq.Consumer.Attributes;
using MessageModels.Entity;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;

namespace Module_02.Task_01.CartingService.WebApi.MessageHandlers.EntityHandlers.Product;

[AcceptMessageType("Product.Updated")]
public class ProductUpdatedMessageHandler : IMessageHandler<ProductMessage>
{
    private readonly IMediator _mediator;
    public ProductUpdatedMessageHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> HandleAsync(MessageHandlerParams<ProductMessage> args, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateBatchCartItemsCommand
            {
                Id = args.Message.ProductId,
                Name = args.Message.Name,
                Price = args.Message.Price,
                ImageUrl = args.Message.Image
            }
            , cancellationToken);

        return result;
    }
}
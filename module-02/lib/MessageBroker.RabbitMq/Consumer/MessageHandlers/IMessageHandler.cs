using Common.MessageBrokerAbstractions;

namespace MessageBroker.RabbitMq.Consumer.MessageHandlers;


public interface IMessageHandler
{
    Task<bool> HandleAsync(object args, CancellationToken cancellationToken);
}

public interface IMessageHandler<TMessage> : IMessageHandler
{
    Task<bool> HandleAsync(MessageHandlerParams<TMessage> args, CancellationToken cancellationToken);

    async Task<bool> IMessageHandler.HandleAsync(object args, CancellationToken cancellationToken)
        => await HandleAsync((MessageHandlerParams<TMessage>)args, cancellationToken);
}
using MessageBroker.RabbitMq.Consumer.Options.ExchangeOptions;
using MessageBroker.RabbitMq.Consumer.Options.QueueOptions;

namespace MessageBroker.RabbitMq.Consumer.Options;

public class DlqProps<TMessage>
{
    public required DlqExchangeOptions<TMessage> Exchange { get; init; }
    public required DlqQueueOptions<TMessage> Queue { get; init; }
}
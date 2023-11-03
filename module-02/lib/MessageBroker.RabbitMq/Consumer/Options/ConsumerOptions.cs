using MessageBroker.RabbitMq.Consumer.Options.ExchangeOptions;
using MessageBroker.RabbitMq.Consumer.Options.QueueOptions;
using MessageBroker.RabbitMq.Producer.Options;

namespace MessageBroker.RabbitMq.Consumer.Options;

public class ConsumerOptions<TMessage>
{
    public ProducerExchangeOptions<TMessage> RootExchange { get; set; }
    public ConnectorExchangeOptions<TMessage> ConnectorExchange { get; set; }
    public List<ConsumerQueueOptions<TMessage>> Consumers { get; set; }
    public DlqProps<TMessage> Dlq { get; set; }
}
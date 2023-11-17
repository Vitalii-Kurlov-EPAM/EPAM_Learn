using MessageBroker.RabbitMq.Consumer.Options;

namespace MessageBroker.RabbitMq.Consumer.OptionsSelector;

public interface IMessageConsumerOptionsSelector
{
    ConsumerOptions<TMessage> GetConsumerOptions<TMessage>();
}
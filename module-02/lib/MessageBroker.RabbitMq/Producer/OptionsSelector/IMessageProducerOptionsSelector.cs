using MessageBroker.RabbitMq.Producer.Options;

namespace MessageBroker.RabbitMq.Producer.OptionsSelector;

public interface IMessageProducerOptionsSelector
{
    ProducerExchangeOptions<TMessage> GetProducerOptions<TMessage>();
}
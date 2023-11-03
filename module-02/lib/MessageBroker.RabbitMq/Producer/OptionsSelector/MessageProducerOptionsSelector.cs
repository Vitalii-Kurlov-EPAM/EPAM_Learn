using MessageBroker.RabbitMq.Producer.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MessageBroker.RabbitMq.Producer.OptionsSelector;

public sealed class MessageProducerOptionsSelector : IMessageProducerOptionsSelector
{
    private readonly IServiceProvider _serviceProvider;
    public MessageProducerOptionsSelector(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ProducerExchangeOptions<TMessage> GetProducerOptions<TMessage>()
    {
        var options = _serviceProvider.GetService<IOptions<ProducerExchangeOptions<TMessage>>>();
        return options.Value;
    }
}
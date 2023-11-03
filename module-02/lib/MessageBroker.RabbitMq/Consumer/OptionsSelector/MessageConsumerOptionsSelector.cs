using MessageBroker.RabbitMq.Consumer.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MessageBroker.RabbitMq.Consumer.OptionsSelector;

public sealed class MessageConsumerOptionsSelector : IMessageConsumerOptionsSelector
{
    private readonly IServiceProvider _serviceProvider;
    public MessageConsumerOptionsSelector(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ConsumerOptions<TMessage> GetConsumerOptions<TMessage>()
    {
        var options = _serviceProvider.GetService<IOptions<ConsumerOptions<TMessage>>>();
        return options.Value;
    }
}
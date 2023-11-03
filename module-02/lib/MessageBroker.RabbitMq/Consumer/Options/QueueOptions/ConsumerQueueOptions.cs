using MessageBroker.RabbitMq.Options.Base;

namespace MessageBroker.RabbitMq.Consumer.Options.QueueOptions;

public class ConsumerQueueOptions<TMessage> : BaseQueueOptions
{
    public override string QueueName =>
        $"{(string.IsNullOrWhiteSpace(AppIdentifier) ? string.Empty : $"{AppIdentifier}-")}queue-{typeof(TMessage).Name}-connector{(string.IsNullOrWhiteSpace(AdditionalQueueNamePart) ? string.Empty : $"-{AdditionalQueueNamePart}")}";
}
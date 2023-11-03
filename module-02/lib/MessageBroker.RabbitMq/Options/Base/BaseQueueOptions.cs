namespace MessageBroker.RabbitMq.Options.Base;

public abstract class BaseQueueOptions
{
    public abstract string QueueName { get; }

    public string AdditionalQueueNamePart { get; set; } = string.Empty;
    public string AppIdentifier { get; set; }

    public bool IsDurable { get; set; } = true;
    public bool IsExclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public int TtlInMilliseconds { get; set; }

    public string RoutingKey { get; set; } = string.Empty;
    public IDictionary<string, object> Headers { get; set; } = null;
}
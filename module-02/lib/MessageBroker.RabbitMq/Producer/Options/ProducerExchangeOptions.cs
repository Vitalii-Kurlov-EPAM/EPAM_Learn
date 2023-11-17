using MessageBroker.RabbitMq.Options.Base;

namespace MessageBroker.RabbitMq.Producer.Options;

public class ProducerExchangeOptions<TMessage> : BaseExchangeOptions
{
    public override string ExchangeType { get; protected set; } = RabbitMQ.Client.ExchangeType.Fanout;

    public override string ExchangeName =>
        $"{(string.IsNullOrWhiteSpace(AppIdentifier) ? string.Empty : $"{AppIdentifier}-")}exchange-{typeof(TMessage).Name}{(string.IsNullOrWhiteSpace(AdditionalExchangeNamePart) ? string.Empty : $"-{AdditionalExchangeNamePart}")}";

    public Func<object, IDictionary<string, object>> GetHeaderArguments { get; set; } = null;

    public Func<object, string> GetRoutingKey { get; set; } = null;
}
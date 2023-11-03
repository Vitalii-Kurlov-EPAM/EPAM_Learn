using MessageBroker.RabbitMq.Options.Base;

namespace MessageBroker.RabbitMq.Consumer.Options.ExchangeOptions;

public class ConnectorExchangeOptions<TMessage>: BaseExchangeOptions
{
    public override string ExchangeType { get; protected set; } = RabbitMQ.Client.ExchangeType.Topic;

    public override string ExchangeName =>
        $"{(string.IsNullOrWhiteSpace(AppIdentifier) ? string.Empty : $"{AppIdentifier}-")}exchange-{typeof(TMessage).Name}-connector{(string.IsNullOrWhiteSpace(AdditionalExchangeNamePart) ? string.Empty : $"-{AdditionalExchangeNamePart}")}";

    public string RoutingKey { get; set; } = string.Empty;
}
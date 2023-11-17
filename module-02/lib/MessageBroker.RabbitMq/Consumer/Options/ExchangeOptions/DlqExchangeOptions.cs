using MessageBroker.RabbitMq.Options.Base;

namespace MessageBroker.RabbitMq.Consumer.Options.ExchangeOptions;

public class DlqExchangeOptions<TMessage>: BaseExchangeOptions
{
    public override string ExchangeType { get; protected set; } = RabbitMQ.Client.ExchangeType.Fanout;

    public override string ExchangeName =>
        $"{(string.IsNullOrWhiteSpace(AppIdentifier) ? string.Empty : $"{AppIdentifier}-")}exchange-{typeof(TMessage).Name}{(string.IsNullOrWhiteSpace(AdditionalExchangeNamePart) ? string.Empty : $"-{AdditionalExchangeNamePart}")}-dlq";
}
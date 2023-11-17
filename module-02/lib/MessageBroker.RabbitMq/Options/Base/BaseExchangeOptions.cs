namespace MessageBroker.RabbitMq.Options.Base;

public abstract class BaseExchangeOptions
{
    public abstract string ExchangeType { get; protected set; }

    public abstract string ExchangeName { get; }

    public string AppIdentifier { get; set; }

    public string AdditionalExchangeNamePart { get; set; } = string.Empty;

    public bool IsDurable { get; set; } = true;

    public bool AutoDelete { get; set; } = false;

    public void SetExchangeType(string exchangeType)
    {
        ExchangeType = exchangeType;
    }
}
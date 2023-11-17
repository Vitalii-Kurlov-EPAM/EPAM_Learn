namespace MessageBroker.RabbitMq.Producer.Options;

public abstract class ExchangeOptions
{
    public abstract string ExchangeType { get; protected set; }

    public abstract string ExchangeName { get; }

    public string AdditionalExchangeNamePart { get; set; } = string.Empty;

    public bool IsExchangeDurable { get; set; } = true;

    public bool AutoDeleteExchange { get; set; } = false;

    public Func<object, IDictionary<string, object>> GetHeaderArguments { get; set; } = null;

    public Func<object, string> GetRoutingKey { get; set; } = null;
}
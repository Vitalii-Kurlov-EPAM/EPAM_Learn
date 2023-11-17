namespace MessageBroker.RabbitMq.Consumer;

public sealed class MessageHandlerMapper
{
    /// <summary>
    /// <para>key -&gt; Message type</para>
    /// <para>value -&gt; Message handler type</para>
    /// </summary>
    public required Dictionary<string, List<(Type Handler, Type Message)>> Mapper { get; init; }
}
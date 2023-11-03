namespace MessageBroker.RabbitMq.Consumer.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AcceptMessageTypeAttribute : Attribute
{
    public string MessageType { get; }

    public AcceptMessageTypeAttribute(string messageType)
    {
        MessageType = messageType;
    }
}
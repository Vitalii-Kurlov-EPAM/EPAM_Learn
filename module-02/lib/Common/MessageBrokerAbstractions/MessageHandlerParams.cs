namespace Common.MessageBrokerAbstractions;

public interface IMessageHandlerParams
{
    public object Message { get; set; }
    
    public IDictionary<string, object> Headers { get; set; }

    public Guid MessageId { get; set; }

    public DateTime TimeStampUtc { get; set; }

    public bool IsHandled { get; set; }
}


public class MessageHandlerParams<TMessage> : IMessageHandlerParams
{
    public TMessage Message { get; set; }

    object IMessageHandlerParams.Message
    {
        get => Message;
        set => Message = (TMessage)value;
    }

    public IDictionary<string, object> Headers { get; set; }

    public Guid MessageId { get; set; }

    public DateTime TimeStampUtc { get; set; }

    public bool IsHandled { get; set; }
}
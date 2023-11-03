namespace Common.MessageBrokerAbstractions.EventArguments;

public class ReceivedMessageEventArgs : EventArgs
{
    public byte[] Message { get; }
    public string ContentType { get; }
    public string ContentEncoding { get; }
    public string MessageType { get; }
    public IDictionary<string, object> Headers { get;}
    public Guid MessageId { get; }
    public DateTime TimeStampUtc { get; }

    public bool IsHandled { get; set; }

    public ReceivedMessageEventArgs(byte[] message, string contentType, string contentEncoding, string messageType , Guid messageId, IDictionary<string, object> headers, DateTime timeStampUtc)
    {
        Message = message;
        ContentType = contentType;
        ContentEncoding = contentEncoding;
        MessageType = messageType;
        MessageId = messageId;
        Headers = headers;
        TimeStampUtc = timeStampUtc;
    }
}
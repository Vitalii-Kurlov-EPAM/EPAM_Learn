namespace Common.MessageBrokerAbstractions.Params;

public record MessageSendParams
(
    ReadOnlyMemory<byte> Message,
    string ContentType,
    string ContentEncoding,
    string MessageType,
    IReadOnlyDictionary<string, object> Headers,
    Guid MessageId,
    TimeSpan TimeStampUtc
);

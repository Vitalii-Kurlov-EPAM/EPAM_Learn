using Common.MessageBrokerAbstractions.Params.Errors;

namespace Common.MessageBrokerAbstractions.Interfaces;

public interface IProducerEventHandler
{
    Task OnMessageSendErrorAsync(MessageSendErrorParams args);
    Task OnConnectionErrorAsync(ConnectionErrorParams args);
}
using Common.MessageBrokerAbstractions.Params.Errors;

namespace Common.MessageBrokerAbstractions.Interfaces;

public interface IProducerEventsHandler
{
    Task OnMessageSendErrorAsync(MessageSendErrorParams args);
    Task OnConnectionErrorAsync(ConnectionErrorParams args);
}
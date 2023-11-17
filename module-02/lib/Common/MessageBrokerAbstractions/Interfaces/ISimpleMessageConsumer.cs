using Common.MessageBrokerAbstractions.EventArguments;

namespace Common.MessageBrokerAbstractions.Interfaces;

public interface ISimpleMessageConsumer<TMessage>
{
    bool StartListening();
    event EventHandler<ReceivedMessageEventArgs> Received;
    event EventHandler Disconnected;
}
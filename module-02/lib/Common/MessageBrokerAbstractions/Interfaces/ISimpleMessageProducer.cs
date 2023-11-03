namespace Common.MessageBrokerAbstractions.Interfaces;

public interface ISimpleMessageProducer<in TMessage>
{
    bool Publish(TMessage message, string messageType, object userObject = null);
}
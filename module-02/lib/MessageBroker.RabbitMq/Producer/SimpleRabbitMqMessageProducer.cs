using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Common.MessageBrokerAbstractions.EventArguments;
using Common.MessageBrokerAbstractions.Interfaces;
using Common.MessageBrokerAbstractions.Params;
using Common.MessageBrokerAbstractions.Params.Errors;
using MessageBroker.RabbitMq.Producer.Options;
using MessageBroker.RabbitMq.Producer.OptionsSelector;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMq.Producer;

public class SimpleRabbitMqMessageProducer<TMessage> : SimpleRabbitMqBase, ISimpleMessageProducer<TMessage>
{
    private readonly ProducerExchangeOptions<TMessage> _exchangeOptions;
    private readonly IProducerEventsHandler _producerEventsHandler;
    public SimpleRabbitMqMessageProducer(IConnectionFactory connectionFactory, 
        IMessageProducerOptionsSelector messageProducerOptionsSelector,
        IProducerEventsHandler producerEventsHandler)
        : base(connectionFactory)
    {
        _exchangeOptions = messageProducerOptionsSelector.GetProducerOptions<TMessage>();
        _producerEventsHandler = producerEventsHandler;
        OpenError += OnOpenError;
    }

    private void PublishConnectionErrorEvent(Exception error)
    {
        Task.Factory.StartNew(() =>
        {
            _producerEventsHandler?.OnConnectionErrorAsync(new ConnectionErrorParams(error));
        });
    }

    private void PublishMessageSendErrorEvent(MessageSendParams messageSendParams, Exception error)
    {
        Task.Factory.StartNew(() =>
        {
            _producerEventsHandler?.OnMessageSendErrorAsync(
                new MessageSendErrorParams(messageSendParams, error)
            );
        });
    }

    private void OnOpenError(object sender, ConnectionOpenErrorArgs e) 
        => PublishConnectionErrorEvent(e.Error);

    protected override void OnOpened()
    {
        Channel.ExchangeDeclare(_exchangeOptions.ExchangeName, ExchangeType.Fanout, _exchangeOptions.IsDurable,
            _exchangeOptions.AutoDelete);
    } 

    private MessageSendParams GetMessageSendParams(TMessage message, string messageType, object userObject = null)
    {
        const string contentType = "application/json";
        const string contentEncoding = "utf-8";
        var messageJson = JsonSerializer.Serialize(message, message.GetType());
        var messageData = Encoding.GetEncoding(contentEncoding).GetBytes(messageJson);
        var messageId = Guid.NewGuid();
        var messageCreatedInUtc = DateTime.UtcNow.Subtract(DateTime.UnixEpoch);
        var headers = _exchangeOptions.GetHeaderArguments?.Invoke(userObject);

        return new MessageSendParams
        (
            messageData,
            contentType,
            contentEncoding,
            messageType,
            headers?.ToImmutableDictionary(),
            messageId,
            messageCreatedInUtc
        );
    }

    private IBasicProperties GetPublishBasicProperies(MessageSendParams messageSendParams)
    {
        var messageProps = Channel.CreateBasicProperties();
        messageProps.Headers = messageSendParams.Headers?.ToDictionary(pair => pair.Key, pair => pair.Value);
        messageProps.Type = messageSendParams.MessageType;
        messageProps.ContentType = messageSendParams.ContentType;
        messageProps.ContentEncoding = messageSendParams.ContentEncoding;
        messageProps.MessageId = messageSendParams.MessageId.ToString("N");
        messageProps.Timestamp = new AmqpTimestamp((long)messageSendParams.TimeStampUtc.TotalSeconds);

        return messageProps;
    } 

    public bool Publish(TMessage message, string messageType, object userObject = null)
    {
        if (!TryOpen())
        {
            return false;
        }

        var messageSendParams = GetMessageSendParams(message, messageType, userObject);
        var messageProps = GetPublishBasicProperies(messageSendParams);
        var routingKey = _exchangeOptions.GetRoutingKey?.Invoke(userObject) ?? string.Empty;

        try
        {
            Channel.BasicPublish(_exchangeOptions.ExchangeName, routingKey, messageProps,
                messageSendParams.Message);
        }
        catch(Exception ex)
        {
            PublishMessageSendErrorEvent(messageSendParams, ex);
            return false;
        }

        return true;
    }
}
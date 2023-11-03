using Common.MessageBrokerAbstractions.EventArguments;
using Common.MessageBrokerAbstractions.Interfaces;
using MessageBroker.RabbitMq.Consumer.Options;
using MessageBroker.RabbitMq.Consumer.Options.QueueOptions;
using MessageBroker.RabbitMq.Consumer.OptionsSelector;
using MessageBroker.RabbitMq.Options.Base;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBroker.RabbitMq.Consumer;

public class SimpleRabbitMqMessageConsumer<TMessage> : SimpleRabbitMqBase, ISimpleMessageConsumer<TMessage>
{
    private readonly ConsumerOptions<TMessage> _consumerOptions;

    public event EventHandler<ReceivedMessageEventArgs> Received;
    protected virtual void OnReceived(ReceivedMessageEventArgs e) => Received?.Invoke(this, e);

    public SimpleRabbitMqMessageConsumer(IConnectionFactory connectionFactory,
        IMessageConsumerOptionsSelector messageConsumerOptionsSelector)
        : base(connectionFactory)
    {
        _consumerOptions = messageConsumerOptionsSelector.GetConsumerOptions<TMessage>();
    }

    private void CreateExchanges(params BaseExchangeOptions[] exchanges)
    {
        foreach (var exchange in exchanges)
        {
            if (exchange == null)
            {
                continue;
            }
            Channel.ExchangeDeclare(exchange.ExchangeName, exchange.ExchangeType,
                exchange.IsDurable, exchange.AutoDelete);
        }
    }

    private void BindExchange(BaseExchangeOptions destinationExchange, BaseExchangeOptions sourceExchange, string routingKey)
    {
        if (destinationExchange == null || sourceExchange == null)
        {
            return;
        }

        Channel.ExchangeBind(destinationExchange.ExchangeName, sourceExchange.ExchangeName,
            routingKey ?? string.Empty);
    }

    private void CreateDlq(DlqProps<TMessage> dlq)
    {
        if (dlq == null)
        {
            return;
        }

        var dlqQueueArgs = new Dictionary<string, object>();
        if (dlq.Queue.TtlInMilliseconds > 0)
        {
            dlqQueueArgs.Add("x-message-ttl", dlq.Queue.TtlInMilliseconds);
        }

        Channel.QueueDeclare(dlq.Queue.QueueName, dlq.Queue.IsDurable,
            dlq.Queue.IsExclusive, dlq.Queue.AutoDelete, dlqQueueArgs);

        Channel.QueueBind(dlq.Queue.QueueName, dlq.Exchange.ExchangeName, dlq.Queue.RoutingKey,
            dlq.Queue.Headers);
    }

    private void CreateConsumer(ConsumerQueueOptions<TMessage> consumerQueue, 
        DlqProps<TMessage> dlq, string bindToExchangeName)
    {
        var consumerQueueArgs = new Dictionary<string, object>();
            
        if (dlq != null)
        {
            consumerQueueArgs.Add("x-dead-letter-exchange", dlq.Exchange.ExchangeName);
        }

        if (consumerQueue.TtlInMilliseconds > 0)
        {
            consumerQueueArgs.Add("x-message-ttl", consumerQueue.TtlInMilliseconds);
        }

        Channel.QueueDeclare(consumerQueue.QueueName, consumerQueue.IsDurable,
            consumerQueue.IsExclusive, consumerQueue.AutoDelete, consumerQueueArgs);

        Channel.QueueBind(consumerQueue.QueueName, bindToExchangeName, consumerQueue.RoutingKey,
            consumerQueue.Headers);

        Channel.BasicQos(0, 1, true);

        var eventingBasicConsumer = new EventingBasicConsumer(Channel);

        eventingBasicConsumer.Received += EventingBasicConsumerOnReceived;
        eventingBasicConsumer.Shutdown += EventingBasicConsumerOnShutdown;
        eventingBasicConsumer.Unregistered += EventingBasicConsumerOnUnregistered;

        Channel.BasicConsume(consumerQueue.QueueName, false, eventingBasicConsumer);
    }
    
    protected override void OnOpened()
    {
        var rootExchange = _consumerOptions.RootExchange;
        var connectorExchange = _consumerOptions.ConnectorExchange;
        var consumers = _consumerOptions.Consumers;
        var dlq = _consumerOptions.Dlq;

        CreateExchanges(rootExchange, connectorExchange, dlq.Exchange);
        BindExchange(connectorExchange, rootExchange, connectorExchange?.RoutingKey);
        CreateDlq(dlq);

        foreach (var consumer in consumers)
        {
            var exchangeName = connectorExchange == null 
                ? rootExchange.ExchangeName
                : connectorExchange.ExchangeName;

            CreateConsumer(consumer, dlq, exchangeName);
        }
    }

    private void UnregisterEventHandlers(EventingBasicConsumer eventingBasicConsumer)
    {
        if (eventingBasicConsumer == null)
        {
            return;
        }

        eventingBasicConsumer.Received -= EventingBasicConsumerOnReceived;
        eventingBasicConsumer.Shutdown -= EventingBasicConsumerOnShutdown;
        eventingBasicConsumer.Unregistered -= EventingBasicConsumerOnUnregistered;
    }

    private void EventingBasicConsumerOnUnregistered(object sender, ConsumerEventArgs e)
    {
        UnregisterEventHandlers((EventingBasicConsumer)sender);
    }

    private void EventingBasicConsumerOnShutdown(object sender, ShutdownEventArgs e)
    {
        UnregisterEventHandlers((EventingBasicConsumer)sender);
    }

    private static ReceivedMessageEventArgs GetReceivedMessageEventArgs(BasicDeliverEventArgs args)
    {
        var timeStamp = DateTime.UnixEpoch.AddSeconds(args.BasicProperties.Timestamp.UnixTime).ToUniversalTime();
        var messageType = args.BasicProperties.Type;
        var contentType = args.BasicProperties.ContentType;
        var contentEncoding = args.BasicProperties.ContentEncoding;
        var messageId = Guid.Parse(args.BasicProperties.MessageId);
        var data = args.Body.ToArray();

        return new ReceivedMessageEventArgs(
            data,
            contentType,
            contentEncoding,
            messageType,
            messageId,
            args.BasicProperties.Headers,
            timeStamp);
    }

    private void EventingBasicConsumerOnReceived(object sender, BasicDeliverEventArgs args)
    {
        var eventArgs = GetReceivedMessageEventArgs(args);

        OnReceived(eventArgs);

        if (eventArgs.IsHandled)
        {
            Channel.BasicAck(args.DeliveryTag, false);
        }
        else
        {
            Channel.BasicNack(args.DeliveryTag, false, false);
        }
    }

    public bool StartListening()
    {
        return TryOpen();
    }
}
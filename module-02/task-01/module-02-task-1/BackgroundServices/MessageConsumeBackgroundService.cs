using System.Text;
using System.Text.Json;
using Common.MessageBrokerAbstractions.EventArguments;
using Common.MessageBrokerAbstractions.Interfaces;
using Common.MessageBrokerAbstractions.MessageHandlers;
using MessageBroker.RabbitMq.Consumer;
using MessageModels.Entity;

namespace Module_02.Task_01.CartingService.WebApi.BackgroundServices;

public class MessageConsumeBackgroundService : BackgroundService
{
    private readonly ISimpleMessageConsumer<IEntityModificationMessage> _messageConsumer;
    private readonly IServiceProvider _serviceProvider;
    private CancellationToken _stoppingToken;
    private ManualResetEventSlim _locker;
    
    public MessageConsumeBackgroundService(ISimpleMessageConsumer<IEntityModificationMessage> messageConsumer,
        IServiceProvider serviceProvider)
    {
        _messageConsumer = messageConsumer;
        _serviceProvider = serviceProvider;
        _messageConsumer.Received += MessageConsumerOnReceived;
        _messageConsumer.Disconnected += MessageConsumerOnDisconnected;
    }

    private IEnumerable<MessageHandlerInfo> GetHandlers(string messageType)
    {
        using var scope = _serviceProvider.CreateScope();
        var messageHandlerMapper = scope.ServiceProvider.GetRequiredService<MessageHandlerMapper>();

        if (!messageHandlerMapper.Mapper.TryGetValue(messageType, out var handlerInfoList))
        {
            yield break;
        }

        foreach (var handlerInfo in handlerInfoList)
        {
            var dataType = handlerInfo.Message;

            var handler = (IMessageHandler)scope.ServiceProvider.GetRequiredService(handlerInfo.Handler);

            var handlerArgsType = typeof(MessageHandlerParams<>).MakeGenericType(dataType);

            yield return new MessageHandlerInfo(handler, handlerArgsType, dataType);
        }
    }

    private async void MessageConsumerOnReceived(object sender, ReceivedMessageEventArgs e)
    {
        foreach (var (handler, handlerArgsType, messageType) in GetHandlers(e.MessageType))
        {
            var jsonMessage = Encoding.GetEncoding(e.ContentEncoding).GetString(e.Message);
            var messageObject = JsonSerializer.Deserialize(jsonMessage, messageType);

            var handlerArgs = (IMessageHandlerParams)Activator.CreateInstance(handlerArgsType);

            handlerArgs.MessageId = e.MessageId;
            handlerArgs.Headers = e.Headers;
            handlerArgs.TimeStampUtc = e.TimeStampUtc;
            handlerArgs.Message = messageObject;

            e.IsHandled = await handler.HandleAsync(handlerArgs, _stoppingToken);
        }
    }

    private ManualResetEventSlim CreateLocker(bool initialState)
    {
        _locker = new ManualResetEventSlim(initialState);
        return _locker;
    }

    private async Task ReconnectWatcher()
    {
        try
        {
            using var locker = CreateLocker(true);

            if (_messageConsumer.StartListening())
            {
                locker.Reset();
                locker.Wait(_stoppingToken);
            }
            
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(5));


            while (await timer.WaitForNextTickAsync(_stoppingToken))
            {
                if (!_messageConsumer.StartListening())
                {
                    continue;
                }

                locker.Reset();
                locker.Wait(_stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            //
        }


    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Factory.StartNew(ReconnectWatcher, stoppingToken);
        return Task.CompletedTask;
    }

    private void MessageConsumerOnDisconnected(object sender, EventArgs e)
    {
        StartAutoConnection();
    }

    private void StartAutoConnection()
    {
        if (!_locker.WaitHandle.SafeWaitHandle.IsClosed &&
            !_locker.IsSet)
        {
            _locker.Set();
        }
    }
}
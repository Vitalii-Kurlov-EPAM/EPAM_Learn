using MessageBroker.RabbitMq.Consumer.Options;
using MessageBroker.RabbitMq.Consumer.Options.ExchangeOptions;
using MessageBroker.RabbitMq.Consumer.Options.QueueOptions;
using MessageBroker.RabbitMq.Extensions;
using MessageBroker.RabbitMq.Producer.Options;
using MessageModels.Entity;
using Module_02.Task_01.CartingService.WebApi.BackgroundServices;
using RabbitMQ.Client;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace Module_02.Task_01.CartingService.WebApi.Configurations;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@127.0.0.1:5672")
        });

        services.RegisterRabbitMqConsumer();
        services.ConfigureConsumerOptions<IEntityModificationMessage>(options =>
        {
            options.RootExchange = new ProducerExchangeOptions<IEntityModificationMessage>
            {
                AppIdentifier = "CatalogService"
            };
            options.ConnectorExchange = new ConnectorExchangeOptions<IEntityModificationMessage>
            {
                AppIdentifier = "CartingService",
                RoutingKey = "entity.*"
            };
            options.Consumers = new List<ConsumerQueueOptions<IEntityModificationMessage>>
            {
                new()
                {
                    RoutingKey = "entity.update",
                    AdditionalQueueNamePart = "update",
                    AppIdentifier = "CartingService",
                    TtlInMilliseconds = (int)TimeSpan.FromSeconds(5).TotalMilliseconds
                }
            };

            options.Dlq = new DlqProps<IEntityModificationMessage>
            {
                Exchange = new DlqExchangeOptions<IEntityModificationMessage>
                {
                    AppIdentifier = "CartingService"
                },
                Queue = new DlqQueueOptions<IEntityModificationMessage>
                {
                    AppIdentifier = "CartingService",
                    TtlInMilliseconds = (int)TimeSpan.FromDays(7).TotalMilliseconds
                }
            };
        });
        services.AddHostedService<MessageConsumeBackgroundService>();
        return services;
    }
}
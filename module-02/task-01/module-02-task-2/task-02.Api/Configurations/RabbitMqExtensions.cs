using Common.MessageBrokerAbstractions.Interfaces;
using MessageBroker.RabbitMq.Extensions;
using MessageModels.Entity;
using Module_02.Task_02.CatalogService.Abstractions.Services.MessageProducers;
using Module_02.Task_02.CatalogService.WebApi.Services;
using Module_02.Task_02.CatalogService.WebApi.Services.MessageProducers.EntityMessageProducer;
using RabbitMQ.Client;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace Module_02.Task_02.CatalogService.WebApi.Configurations;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@127.0.0.1:5672")
        });

        services.AddScoped<IEntityMessageProducer, EntityMessageProducer>();

        services.RegisterRabbitMqProducer();
        services.ConfigureProducerOptions<IEntityModificationMessage>(options =>
        {
            options.AppIdentifier = "CatalogService";
            options.GetRoutingKey += o =>
            {
                return o switch
                {
                    "add" => "entity.add",
                    "update" => "entity.update",
                    "delete" => "entity.delete",
                    _ => $"entity.unknown.{o}"
                };
            };
        });

        services.AddSingleton<IProducerEventsHandler, ProducerEventsHandler>();
        return services;
    }
}
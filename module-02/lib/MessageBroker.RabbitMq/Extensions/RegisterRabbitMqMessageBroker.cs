using System.Reflection;
using Common.MessageBrokerAbstractions.Interfaces;
using Common.MessageBrokerAbstractions.MessageHandlers;
using MessageBroker.RabbitMq.Consumer;
using MessageBroker.RabbitMq.Consumer.Attributes;
using MessageBroker.RabbitMq.Consumer.Options;
using MessageBroker.RabbitMq.Consumer.OptionsSelector;
using MessageBroker.RabbitMq.Producer;
using MessageBroker.RabbitMq.Producer.Options;
using MessageBroker.RabbitMq.Producer.OptionsSelector;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker.RabbitMq.Extensions;

public static class RegisterRabbitMqMessageBrokerExtensions
{
    #region Producer

    public static IServiceCollection RegisterRabbitMqProducer(this IServiceCollection services)
    {
        services.AddSingleton(typeof(ISimpleMessageProducer<>), typeof(SimpleRabbitMqMessageProducer<>));
        services.AddSingleton<IMessageProducerOptionsSelector, MessageProducerOptionsSelector>();
        services.AddSingleton<IProducerEventsHandler>(_ => null);

        return services;
    }

    public static IServiceCollection ConfigureProducerOptions<TMessage>(this IServiceCollection services,
        Action<ProducerExchangeOptions<TMessage>> configureAction)
    {
        return services.Configure(configureAction);
    }

    #endregion

    #region Consumer

    public static IServiceCollection RegisterRabbitMqConsumer(this IServiceCollection services)
    {
        services.AddSingleton(typeof(ISimpleMessageConsumer<>), typeof(SimpleRabbitMqMessageConsumer<>));
        services.AddSingleton<IMessageConsumerOptionsSelector, MessageConsumerOptionsSelector>();

        var messageHandlerTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => GetAllHandlersFrom(typeof(IMessageHandler<>), t));

        var handlerMapper = new Dictionary<string, List<(Type Handler, Type Message)>>();

        foreach (var (handlerType, messageType, messageNames) in messageHandlerTypes)
        {
            var genericServiceType = typeof(IMessageHandler<>).MakeGenericType(messageType);
            services.AddScoped(genericServiceType, handlerType);
            services.AddScoped(handlerType, handlerType);

            foreach (var messageName in messageNames)
            {
                if (!handlerMapper.TryGetValue(messageName, out var handlers))
                {
                    handlers = new List<(Type Handler, Type Message)>();
                    handlerMapper[messageName] = handlers;
                }

                handlers.Add((handlerType, messageType));
            }
        }

        var messageHandlerMapper = new MessageHandlerMapper
        {
            Mapper = handlerMapper
        };

        services.AddSingleton(_ => messageHandlerMapper);

        return services;
    }

    private static IEnumerable<(Type HandlerType, Type MessageType, string[] MessageNames)>
        GetAllHandlersFrom(Type openGenericType, Assembly assembly)
    {
        return from type in assembly.GetTypes()
            from implementedInterface in type.GetInterfaces()
            let baseType = type.BaseType
            where
                (baseType is { IsGenericType: true } &&
                 openGenericType.IsAssignableFrom(baseType.GetGenericTypeDefinition())) ||
                (implementedInterface.IsGenericType &&
                 openGenericType.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
            select (
                type,
                implementedInterface.GetGenericArguments()[0],
                type.GetCustomAttributes<AcceptMessageTypeAttribute>().Select(attr => attr.MessageType).ToArray());
    }

    public static IServiceCollection ConfigureConsumerOptions<TMessage>(this IServiceCollection services,
        Action<ConsumerOptions<TMessage>> configureAction)
    {
        return services.Configure(configureAction);
    }

    #endregion
}
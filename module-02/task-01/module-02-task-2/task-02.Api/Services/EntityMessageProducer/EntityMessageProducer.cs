using Common.MessageBrokerAbstractions.Interfaces;
using MessageModels;
using Module_02.Task_02.CatalogService.Abstractions.Services;

namespace Module_02.Task_02.CatalogService.WebApi.Services.EntityMessageProducer;

public class EntityMessageProducer : IEntityMessageProducer
{
    private readonly ISimpleMessageProducer<IEntityModificationMessage> _messageProducer;

    public EntityMessageProducer(ISimpleMessageProducer<IEntityModificationMessage> messageProducer)
    {
        _messageProducer = messageProducer;
    }

    public bool PublishEntityAdded(IEntityModificationMessage message, string entityName) 
        => _messageProducer.Publish(message, $"{entityName}.Added", "add");

    public bool PublishEntityUpdates(IEntityModificationMessage message, string entityName) 
        => _messageProducer.Publish(message, $"{entityName}.Updated", "update");

    public bool PublishEntityDeleted(IEntityModificationMessage message, string entityName) 
        => _messageProducer.Publish(message, $"{entityName}.Deleted", "delete");
}
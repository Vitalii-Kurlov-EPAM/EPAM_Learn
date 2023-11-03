using MessageModels.Entity;

namespace Module_02.Task_02.CatalogService.Abstractions.Services.MessageProducers;

public interface IEntityMessageProducer
{
    bool PublishEntityAdded(IEntityModificationMessage message, string entityName);
    bool PublishEntityUpdated(IEntityModificationMessage message, string entityName);
    bool PublishEntityDeleted(IEntityModificationMessage message, string entityName);
}
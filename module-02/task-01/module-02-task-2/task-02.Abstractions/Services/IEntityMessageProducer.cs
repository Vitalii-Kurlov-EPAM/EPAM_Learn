using MessageModels;

namespace Module_02.Task_02.CatalogService.Abstractions.Services;

public interface IEntityMessageProducer
{
    bool PublishEntityAdded(IEntityModificationMessage message, string entityName);
    bool PublishEntityUpdates(IEntityModificationMessage message, string entityName);
    bool PublishEntityDeleted(IEntityModificationMessage message, string entityName);
}
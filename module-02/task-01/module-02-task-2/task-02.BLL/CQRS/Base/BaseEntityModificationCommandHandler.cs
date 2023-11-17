using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.Services.MessageProducers;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.Base;

public abstract class BaseEntityModificationCommandHandler : BaseCommandHandler
{
    protected IEntityMessageProducer EntityMessageProducer { get; }

    protected BaseEntityModificationCommandHandler(
        IWithModificationsDbContext dbContext, ILogger logger,
        IEntityMessageProducer entityMessageProducer)
        : base(dbContext, logger)
    {
        EntityMessageProducer = entityMessageProducer;
    }
}
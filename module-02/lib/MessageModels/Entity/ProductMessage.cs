namespace MessageModels.Entity;

public record ProductMessage(
    int ProductId,
    string Name,
    string Description,
    string Image,
    int CategoryId,
    decimal Price,
    int Amount
) : IEntityModificationMessage;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.DatabaseContext;

public interface IDbContextModification
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
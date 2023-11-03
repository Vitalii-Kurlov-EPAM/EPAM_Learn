namespace Module_02.Task_02.CatalogService.Abstractions.DB.Repositories.Base;

public interface IBaseCrudRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeps = false, CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(int id, bool includeDeps = false, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
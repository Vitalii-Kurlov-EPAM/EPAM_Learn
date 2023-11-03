namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories.Base;

public interface IBaseCrudRepository<TEntity>
{
    IEnumerable<TEntity> GetAll(bool includeDeps = false);

    TEntity GetById(int id, bool includeDeps = false);

    bool DeleteById(int id);

    bool Delete(TEntity entity);

    int Add(TEntity entity);

    bool Update(TEntity entity);
}
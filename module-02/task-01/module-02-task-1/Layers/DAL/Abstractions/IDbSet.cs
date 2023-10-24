using System.Linq.Expressions;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;

public interface IDbSet<TEntity>
    where TEntity : class, new()
{
    IDbSet<TEntity> Include<TField>(params Expression<Func<TEntity, TField>>[] keySelectors);
    TEntity GetById(object id);
    bool Any(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> All();
    object Insert(TEntity value);
    bool Delete(TEntity value);
    bool Delete(object id);
    bool Update(TEntity value);
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;
using LiteDB;

namespace Module_02.Task_01.CartingService.WebApi.DAL.DbEngine.LiteDb;

public class LiteDbSet<TEntity>: Abstractions.IDbSet<TEntity>
    where TEntity : class, new()
{
    private readonly ILiteCollection<TEntity> _currentCollection;
    private readonly ILiteDatabase _database;

    public LiteDbSet(ILiteDatabase database, ILiteCollection<TEntity> currentCollection = null)
    {
        _database = database;
        _currentCollection = currentCollection ?? _database.GetCollection<TEntity>(GetCollectionName());
    }

    private static string GetCollectionName()
    {
        var type = typeof(TEntity);
        return type.GetCustomAttribute<TableAttribute>()?.Name ?? type.Name;
    }

    private static PropertyInfo GetIdProp()
    {
        var idProp = typeof(TEntity).GetProperties().FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);
        if (idProp == null)
        {
            throw new ArgumentException(
                "The passed value does not contain an ID property that allows the entry to be deleted.\n" +
                $"One of all properties must be marked with the \"{typeof(KeyAttribute).FullName}\" attribute.");
        }

        return idProp;
    }

    public Abstractions.IDbSet<TEntity> Include<TField>(params Expression<Func<TEntity, TField>>[] keySelectors)
    {
        var newCollection = _currentCollection;
        foreach (var keySelector in keySelectors)
        {
            newCollection = newCollection.Include(keySelector);
        }

        return new LiteDbSet<TEntity>(_database, newCollection);
    }

    public TEntity GetById(object id) 
        => _currentCollection.FindById(new BsonValue(id));

    public bool Any(Expression<Func<TEntity, bool>> predicate)
        => _currentCollection.Exists(predicate);

    public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        => _currentCollection.Find(predicate);

    public IEnumerable<TEntity> All()
        => _currentCollection.FindAll();

    public object Insert(TEntity value) 
        => _currentCollection.Insert(value)?.RawValue;

    public bool Delete(TEntity value)
    {
        var idProp = GetIdProp();
        var id = idProp.GetValue(value);
        return Delete(id);
    }

    public bool Delete(object id) 
        => _currentCollection.Delete(new BsonValue(id));

    public bool Update(TEntity value)
    {
        var idProp = GetIdProp();
        var id = idProp.GetValue(value);
        return _currentCollection.Update(new BsonValue(id), value);
    }
}
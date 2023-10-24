using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using LiteDB;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.DbEngine.LiteDb;

public class LiteDbSet<TEntity> : IDbSet<TEntity>
    where TEntity : class, new()
{
    private readonly ILiteCollection<TEntity> _currentCollection;
    private readonly ILiteDatabase _database;


    public LiteDbSet(ILiteDatabase database, string collectionName)
        : this(database, database.GetCollection<TEntity>(collectionName))
    {
    }

    private LiteDbSet(ILiteDatabase database, ILiteCollection<TEntity> currentCollection)
    {
        _database = database;
        _currentCollection = currentCollection;
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

    public IDbSet<TEntity> Include<TField>(params Expression<Func<TEntity, TField>>[] keySelectors)
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
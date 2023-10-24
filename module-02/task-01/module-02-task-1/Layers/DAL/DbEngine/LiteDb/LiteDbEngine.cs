using LiteDB;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;

namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.DbEngine.LiteDb;

public abstract class LiteDbEngine : Disposable
{
    private readonly IDbConnectionSettings _dbConnectionSettings;

    private ILiteDatabase _database;

    public bool IsTransactionTaken { get; private set; }

    public bool IsOpen => _database != null;

    protected LiteDbEngine(IDbConnectionSettings dbConnectionSettings)
    {
        _dbConnectionSettings = dbConnectionSettings;
    }

    private void OpenConnection()
        => _database ??= new LiteDatabase(_dbConnectionSettings.ConnectionString);

    private void CloseConnection()
    {
        RollbackChanges();
        _database?.Dispose();
        _database = null;
    }

    protected IDbSet<TEntity> GetDbSet<TEntity>(string collectionName)
        where TEntity : class, new()
    {
        OpenConnection();
        return new LiteDbSet<TEntity>(_database, collectionName);
    }

    public bool BeginTransaction()
    {
        if (IsTransactionTaken)
        {
            return false;
        }

        OpenConnection();
        IsTransactionTaken = _database.BeginTrans();
        return IsTransactionTaken;
    }

    public bool CommitChanges()
    {
        if (!IsTransactionTaken)
        {
            return false;
        }

        var result = _database.Commit();
        IsTransactionTaken = false;

        return result;
    }

    public bool RollbackChanges()
    {
        if (!IsTransactionTaken)
        {
            return false;
        }

        var result = _database.Rollback();
        IsTransactionTaken = false;

        return result;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            CloseConnection();
        }

        base.Dispose(disposing);
    }
}
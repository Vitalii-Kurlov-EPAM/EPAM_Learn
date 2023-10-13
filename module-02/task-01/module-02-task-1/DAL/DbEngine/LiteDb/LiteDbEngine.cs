using LiteDB;
using Module_02.Task_01.CartingService.WebApi.DAL.Abstractions;
using Сommon;
using Сommon.DB.Abstractions;

namespace Module_02.Task_01.CartingService.WebApi.DAL.DbEngine.LiteDb;

public class LiteDbEngine : Disposable
{
    private readonly IDbConnectionSettings _dbConnectionSettings;

    private ILiteDatabase _database;

    private bool _isTransactionTaken;
    public bool IsTransactionTaken => _isTransactionTaken;

    public  bool IsOpen => _database != null;

    public LiteDbEngine(IDbConnectionSettings dbConnectionSettings)
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

    public IDbSet<TEntity> GetDbSet<TEntity>()
        where TEntity : class, new()
    {
        OpenConnection();
        return new LiteDbSet<TEntity>(_database);
    }

    public bool BeginTransaction()
    {
        if (IsTransactionTaken)
        {
            return false;
        }

        OpenConnection();
        _isTransactionTaken = _database.BeginTrans();
        return IsTransactionTaken;
    }

    public bool CommitChanges()
    {
        if (!IsTransactionTaken)
        {
            return false;
        }

        var result = _database.Commit();
        _isTransactionTaken = false;
        
        return result;
    }

    public bool RollbackChanges()
    {
        if (!IsTransactionTaken)
        {
            return false;
        }

        var result = _database.Rollback();
        _isTransactionTaken = false;

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
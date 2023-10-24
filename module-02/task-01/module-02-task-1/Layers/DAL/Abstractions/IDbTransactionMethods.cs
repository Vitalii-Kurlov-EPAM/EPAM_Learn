namespace Module_02.Task_01.CartingService.WebApi.Layers.DAL.Abstractions;

public interface IDbTransactionMethods
{
    bool BeginTransaction();
    bool CommitChanges();
    bool RollbackChanges();
}
namespace Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;

public class DbRecordNotFoundException : Exception
{
    public DbRecordNotFoundException(string message)
        : base(message)
    {
    }
}
namespace Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;

public class DbRecordAlreadyExistException : Exception
{
    public DbRecordAlreadyExistException(string message)
        : base(message)
    {
    }
}
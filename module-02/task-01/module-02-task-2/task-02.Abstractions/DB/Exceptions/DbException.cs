namespace Module_02.Task_02.CatalogService.Abstractions.DB.Exceptions;

public class DbException : Exception
{
    public DbException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
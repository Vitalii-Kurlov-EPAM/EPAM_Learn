namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Exceptions;

public class DbException : Exception
{
    public DbException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
namespace Сommon.DB.Exceptions;

public class DbException : Exception
{
    public DbException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
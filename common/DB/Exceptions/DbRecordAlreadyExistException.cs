namespace Сommon.DB.Exceptions;

public class DbRecordAlreadyExistException : Exception
{
    public DbRecordAlreadyExistException(string message)
        : base(message)
    {
    }
}
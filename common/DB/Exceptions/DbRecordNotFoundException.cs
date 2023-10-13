namespace Сommon.DB.Exceptions;

public class DbRecordNotFoundException : Exception
{
    public DbRecordNotFoundException(string message)
        : base(message)
    {
    }
}
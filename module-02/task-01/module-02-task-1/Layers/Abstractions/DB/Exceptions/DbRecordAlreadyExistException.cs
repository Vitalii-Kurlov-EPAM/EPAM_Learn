namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Exceptions;

public class DbRecordAlreadyExistException : Exception
{
    public DbRecordAlreadyExistException(string message)
        : base(message)
    {
    }
}
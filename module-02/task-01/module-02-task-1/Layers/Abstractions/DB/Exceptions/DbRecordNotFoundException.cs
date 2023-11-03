namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Exceptions;

public class DbRecordNotFoundException : Exception
{
    public DbRecordNotFoundException(string message)
        : base(message)
    {
    }
}
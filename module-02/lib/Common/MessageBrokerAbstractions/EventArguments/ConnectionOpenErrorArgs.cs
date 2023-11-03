namespace Common.MessageBrokerAbstractions.EventArguments;

public class ConnectionOpenErrorArgs : EventArgs
{
    public Exception Error { get; }

    public ConnectionOpenErrorArgs(Exception error)
    {
        Error = error;
    }
}
using Common;
using Common.MessageBrokerAbstractions.EventArguments;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMq;

public abstract class SimpleRabbitMqBase : Disposable
{
    private readonly IConnectionFactory _connectionFactory;

    public event EventHandler Disconnected;
    protected virtual void OnDisconnected() 
        => Disconnected?.Invoke(this, EventArgs.Empty);

    public event EventHandler<ConnectionOpenErrorArgs> OpenError;
    protected virtual void OnOpenError(ConnectionOpenErrorArgs e)
        => OpenError?.Invoke(this, e);

    protected IModel Channel { get; private set; }
        
    protected IConnection Connection;

    public bool IsOpened => Connection?.IsOpen == true && Channel?.IsOpen == true;

    protected SimpleRabbitMqBase(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    protected abstract void OnOpened();

    private bool TryOpenConnection()
    {
        if (Connection?.IsOpen == true)
        {
            return true;
        }

        Connection?.Dispose();

        try
        {
            Connection = _connectionFactory.CreateConnection();
        }
        catch(Exception ex)
        {
            OnOpenError(new ConnectionOpenErrorArgs(ex));
            return false;
        }

        return true;
    }

    private bool TryOpenChannel()
    {
        if (Channel?.IsOpen == true)
        {
            return true;
        }

        Channel?.Dispose();

        try
        {
            Channel = Connection.CreateModel();
        }
        catch(Exception ex)
        {
            OnOpenError(new ConnectionOpenErrorArgs(ex));
            return false;
        }

        return true;
    }

    protected bool TryOpen()
    {
        if (IsOpened)
        {
            return true;
        }

        if (!TryOpenConnection() || !TryOpenChannel()) return false;
            
        OnOpened();

        Connection.ConnectionShutdown += ConnectionOnConnectionShutdown;
            
        return true;
    }

    private void ConnectionOnConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        OnDisconnected();
    }

    private void DisposeConnection()
    {
        Connection.ConnectionShutdown -= ConnectionOnConnectionShutdown;
        Channel?.Dispose();
        Connection?.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            DisposeConnection();
        }

        base.Dispose(disposing);
    }
}
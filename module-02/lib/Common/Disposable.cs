namespace Common;

public abstract class Disposable : IDisposable
{
    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // Dispose any managed objects
        }

        // Now disposed of any unmanaged objects

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Disposable()
    {
        Dispose(false);
    }
}
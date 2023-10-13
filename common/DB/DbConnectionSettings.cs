using Сommon.DB.Abstractions;

namespace Сommon.DB;

public class DbConnectionSettings : IDbConnectionSettings
{
    public string ConnectionString { get; set; }
}
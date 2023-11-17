namespace Module_02.Task_02.CatalogService.Abstractions.DB;

public class DbConnectionSettings : IDbConnectionSettings
{
    public string ConnectionString { get; set; }
}
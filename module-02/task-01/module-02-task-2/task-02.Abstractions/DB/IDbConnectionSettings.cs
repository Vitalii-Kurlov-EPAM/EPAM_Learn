namespace Module_02.Task_02.CatalogService.Abstractions.DB;

public interface IDbConnectionSettings
{
    string ConnectionString { get; set; }
}
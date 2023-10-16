namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB;

public class DbConnectionSettings : IDbConnectionSettings
{
    public string ConnectionString { get; set; }
}
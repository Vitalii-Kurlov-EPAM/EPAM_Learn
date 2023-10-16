namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB;

public interface IDbConnectionSettings
{
    string ConnectionString { get; set; }
}
using Module_02.Task_02.CatalogService.Abstractions.DB.Repositories;

namespace Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;

public interface IDbContext
{
    ICategoryRepository CategoryRepository { get; }

    IProductRepository ProductRepository { get; }
}
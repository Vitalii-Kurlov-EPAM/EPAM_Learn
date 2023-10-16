using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;
using Module_02.Task_02.CatalogService.Abstractions.DB.Repositories.Base;

namespace Module_02.Task_02.CatalogService.Abstractions.DB.Repositories;

public interface ICategoryRepository : IBaseCrudRepository<CategoryEntity>, IBaseItemExistRepository
{
    Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default);
}
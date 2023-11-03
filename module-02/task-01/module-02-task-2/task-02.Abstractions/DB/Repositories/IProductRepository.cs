using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;
using Module_02.Task_02.CatalogService.Abstractions.DB.Repositories.Base;

namespace Module_02.Task_02.CatalogService.Abstractions.DB.Repositories;

public interface IProductRepository : IBaseCrudRepository<ProductEntity>, IBaseItemExistRepository
{
    Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> IsExistByNameAsync(string name, int ignoreProductId, CancellationToken cancellationToken = default);
    Task<PagedResult<ProductEntity>> GetProductsByCategoryAsync(int categoryId, int page = 1, int pageSize = 10, bool includeDeps = false, CancellationToken cancellationToken = default);
}
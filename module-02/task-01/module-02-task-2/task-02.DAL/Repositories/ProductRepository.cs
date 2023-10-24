using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.CatalogService.Abstractions.DB;
using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;
using Module_02.Task_02.CatalogService.Abstractions.DB.Repositories;
using Module_02.Task_02.CatalogService.DAL.Context;
using Module_02.Task_02.CatalogService.DAL.Extensions;

namespace Module_02.Task_02.CatalogService.DAL.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    private DbSet<ProductEntity> DbSet => DbContext.Products;

    public ProductRepository(IDbSetContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync(bool includeDeps = false, CancellationToken cancellationToken = default)
    {
        IQueryable<ProductEntity> dbSet = includeDeps
            ? DbSet.Include(entity => entity.Category)
            : DbSet;

        return await dbSet.ToArrayAsync(cancellationToken);
    }

    public async Task<ProductEntity> GetByIdAsync(int id, bool includeDeps = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ProductEntity> dbSet = includeDeps
            ? DbSet.Include(entity => entity.Category)
            : DbSet;

        return await dbSet.SingleOrDefaultAsync(entity => entity.ProductId == id, cancellationToken);
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.SingleOrDefaultAsync(entity => entity.ProductId == id, cancellationToken);
        
        return await DeleteAsync(entity, cancellationToken);
    }

    public Task<bool> DeleteAsync(ProductEntity entity, CancellationToken cancellationToken = default)
    {
        var result = DbSet.Remove(entity);

        return Task.FromResult(result.State == EntityState.Deleted);
    }

    public async Task<int> AddAsync(ProductEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity.CategoryId;
    }

    public Task<bool> UpdateAsync(ProductEntity entity, CancellationToken cancellationToken = default)
    {
        var result = DbSet.Update(entity);

        return Task.FromResult(result.State == EntityState.Modified);
    }

    public async Task<bool> IsExistByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(entity => entity.CategoryId == id, cancellationToken);
    }

    public async Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(entity => entity.Name == name, cancellationToken);
    }

    public Task<PagedResult<ProductEntity>> GetProductsByCategoryAsync(int categoryId, int page = 1, int pageSize = 10, bool includeDeps = false, CancellationToken cancellationToken = default)
    {
        IQueryable<ProductEntity> dbSet = includeDeps
            ? DbSet.Include(entity => entity.Category)
            : DbSet;

        return dbSet.Where(entity => entity.CategoryId == categoryId).GetPagedAsync(page, pageSize);
    }
}
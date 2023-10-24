using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;
using Module_02.Task_02.CatalogService.Abstractions.DB.Repositories;
using Module_02.Task_02.CatalogService.DAL.Context;

namespace Module_02.Task_02.CatalogService.DAL.Repositories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    private DbSet<CategoryEntity> DbSet => DbContext.Categories;

    public CategoryRepository(IDbSetContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<CategoryEntity>> GetAllAsync(bool includeDeps = false, CancellationToken cancellationToken = default)
    {
        IQueryable<CategoryEntity> dbSet = includeDeps
            ? DbSet.Include(entity => entity.ParentCategory)
            : DbSet;

        return await dbSet.ToArrayAsync(cancellationToken);
    }

    public async Task<CategoryEntity> GetByIdAsync(int id, bool includeDeps = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<CategoryEntity> dbSet = includeDeps
            ? DbSet.Include(entity => entity.ParentCategory)
            : DbSet;

        return await dbSet.SingleOrDefaultAsync(entity => entity.CategoryId == id, cancellationToken);
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.SingleOrDefaultAsync(entity => entity.CategoryId == id, cancellationToken);
        
        return await DeleteAsync(entity, cancellationToken);
    }

    public Task<bool> DeleteAsync(CategoryEntity entity, CancellationToken cancellationToken = default)
    {
        var result = DbSet.Remove(entity);

        return Task.FromResult(result.State == EntityState.Deleted);
    }

    public async Task<int> AddAsync(CategoryEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity.CategoryId;
    }

    public Task<bool> UpdateAsync(CategoryEntity entity, CancellationToken cancellationToken = default)
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
}
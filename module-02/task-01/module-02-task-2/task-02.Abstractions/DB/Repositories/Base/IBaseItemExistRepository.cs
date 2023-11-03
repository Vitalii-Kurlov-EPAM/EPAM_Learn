namespace Module_02.Task_02.CatalogService.Abstractions.DB.Repositories.Base;

public interface IBaseItemExistRepository
{
    Task<bool> IsExistByIdAsync(int id, CancellationToken cancellationToken = default);
}
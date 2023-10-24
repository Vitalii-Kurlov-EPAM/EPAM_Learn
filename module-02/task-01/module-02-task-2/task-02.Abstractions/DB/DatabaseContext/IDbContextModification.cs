﻿namespace Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;

public interface IDbContextModification
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
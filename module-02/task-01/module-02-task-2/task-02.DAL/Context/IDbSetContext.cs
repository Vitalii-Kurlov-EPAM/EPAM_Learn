using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.CatalogService.Abstractions.DB.Entities;

namespace Module_02.Task_02.CatalogService.DAL.Context;

public interface IDbSetContext
{
    DbSet<ProductEntity> Products { get; set; }
    DbSet<CategoryEntity> Categories { get; set; }
}
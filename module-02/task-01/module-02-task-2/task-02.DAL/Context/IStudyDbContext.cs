using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.DAL.Context;

public interface IStudyDbContext 
{
    DbSet<ProductEntity> Products { get; }
    DbSet<CategoryEntity> Categories { get; }
    DatabaseFacade Database { get; }
}
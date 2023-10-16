using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.CatalogService.Abstractions.DB;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;

namespace Module_02.Task_02.CatalogService.DAL.Context;

public class StudyReadOnlyDbContext : WithTrackingStudyDbContext, IReadOnlyDbContext
{
    public StudyReadOnlyDbContext(IDbConnectionSettings connectionStrings) 
        : base(connectionStrings)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}
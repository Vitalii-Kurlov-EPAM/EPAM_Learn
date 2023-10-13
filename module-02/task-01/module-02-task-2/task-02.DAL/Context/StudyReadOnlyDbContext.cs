using Microsoft.EntityFrameworkCore;
using Сommon.DB.Abstractions;

namespace Module_02.Task_02.DAL.Context;

public class StudyReadOnlyDbContext : WithTrackingStudyDbContext, IStudyReadOnlyDbContext
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
namespace Module_02.Task_02.DAL.Context;

public interface IWithTrackingStudyDbContext : IStudyDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
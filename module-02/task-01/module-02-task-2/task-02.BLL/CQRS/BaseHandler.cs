using MediatR;
using Module_02.Task_02.DAL.Context;

namespace Module_02.Task_02.BLL.CQRS;

public abstract class BaseHandler<TStudyDbContext>
    where TStudyDbContext : IStudyDbContext
{
    protected IMediator Mediator { get; }
    protected TStudyDbContext DbContext { get; }

    protected BaseHandler(IMediator mediator, TStudyDbContext dbContext)
    {
        DbContext = dbContext;
        Mediator = mediator;
    }
}
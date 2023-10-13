using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Queries;

public sealed class CheckProductIsExistByIdQueryHandler : BaseProductQueryHandler,
    IRequestHandler<CheckProductIsExistByIdQuery, bool>
{
    public CheckProductIsExistByIdQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<bool> Handle(CheckProductIsExistByIdQuery query, CancellationToken cancellationToken)
    {
        return await ProductsDbSet.AnyAsync(entity => entity.ProductId == query.ProductId, cancellationToken);
    }
}
using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.CategoryObject.QueryHandlers;

public sealed class CheckCategoryIsExistByIdQueryHandler : BaseQueryHandler,
    IRequestHandler<CheckCategoryIsExistByIdQuery, bool>
{
    public CheckCategoryIsExistByIdQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<bool> Handle(CheckCategoryIsExistByIdQuery query, CancellationToken cancellationToken)
    {
        return await DbContext.CategoryRepository.IsExistByIdAsync(query.CategoryId, cancellationToken);
    }
}
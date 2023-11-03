using MediatR;
using Microsoft.Extensions.Logging;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.CategoryObject.QueryHandlers;

public sealed class GetCategoryByIdQueryHandler : BaseQueryHandler,
    IRequestHandler<GetCategoryByIdQuery, CategoryObjectModels.ItemModel>
{
    public GetCategoryByIdQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext, ILogger<GetCategoryByIdQueryHandler> logger)
        : base(mediator, dbContext, logger)
    {
    }

    public async Task<CategoryObjectModels.ItemModel> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await DbContext.CategoryRepository.GetByIdAsync(query.CategoryId, query.IncludeDeps, cancellationToken);
        return category.ToCategoryItemModel();
    }
}
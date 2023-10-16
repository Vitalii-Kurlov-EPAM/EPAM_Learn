using MediatR;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject;
using Module_02.Task_02.CatalogService.Abstractions.CQRS.CategoryObject.Queries;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.Abstractions.Extensions.ModelMapping;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;

namespace Module_02.Task_02.CatalogService.BLL.CQRS.CategoryObject.QueryHandlers;

public sealed class GetAllCategoriesQueryHandler : BaseQueryHandler, IRequestHandler<GetAllCategoriesQuery, CategoryObjectModels.ItemModel[]>
{
    public GetAllCategoriesQueryHandler(IMediator mediator, IReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<CategoryObjectModels.ItemModel[]> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await DbContext.CategoryRepository.GetAllAsync(query.IncludeDeps, cancellationToken);
        return categories.Select(entity => entity.ToCategoryItemModel()).ToArray();
    }
}
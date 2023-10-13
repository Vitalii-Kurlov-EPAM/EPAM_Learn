using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.BLL.MappingExtensions;
using Module_02.Task_02.BLL.Models;
using Module_02.Task_02.DAL.Context;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Queries;

public sealed class GetAllProductsQueryHandler : BaseProductQueryHandler, IRequestHandler<GetAllProductsQuery, Product.ItemModel[]>
{
    public GetAllProductsQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<Product.ItemModel[]> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<ProductEntity> productsDbSet =
            query.IncludeDependencies
                ? ProductsDbSet.Include(entity => entity.Category)
                : ProductsDbSet;

        var products = await productsDbSet.Select(entity => entity.ToProductItemModel()).ToArrayAsync(cancellationToken);
        return products;
    }
}
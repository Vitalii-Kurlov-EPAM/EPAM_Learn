using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.BLL.MappingExtensions;
using Module_02.Task_02.BLL.Models;
using Module_02.Task_02.DAL.Context;
using Module_02.Task_02.DAL.Entities;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Queries;

public sealed class GetProductByIdQueryHandler : BaseProductQueryHandler,
    IRequestHandler<GetProductByIdQuery, Product.ItemModel>
{
    public GetProductByIdQueryHandler(IMediator mediator, IStudyReadOnlyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<Product.ItemModel> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        IQueryable<ProductEntity> productsDbSet =
            query.IncludeDependencies
                ? ProductsDbSet.Include(entity => entity.Category)
                : ProductsDbSet;


        var product =
            await productsDbSet.SingleOrDefaultAsync(entity => entity.ProductId == query.ProductId,
                cancellationToken);
        return product.ToProductItemModel();
    }

}
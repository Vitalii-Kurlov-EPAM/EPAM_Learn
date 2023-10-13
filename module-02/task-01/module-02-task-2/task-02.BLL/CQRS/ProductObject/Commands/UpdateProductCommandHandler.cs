using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;
using Module_02.Task_02.DAL.Context;
using Сommon.DB.Exceptions;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Commands;

public sealed class UpdateProductCommandHandler : BaseProductCommandHandler, IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IMediator _mediator;

    public UpdateProductCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
        _mediator = mediator;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (!await _mediator.Send(new CheckCategoryIsExistByIdQuery(request.CategoryId), cancellationToken))
        {
            throw new DbRecordNotFoundException(
                $"Can not update Product. The passed category_id={request.CategoryId} not found.");

        }

        var foundProduct =
            await ProductsDbSet.SingleOrDefaultAsync(entity => entity.ProductId == request.ProductId,
                cancellationToken);

        if (foundProduct == null)
        {
            throw new DbRecordNotFoundException(
                $"Can not update Product. The passed product_id={request.ProductId} not found.");
        }

        foundProduct.CategoryId = request.CategoryId;
        foundProduct.Name = request.Name;
        foundProduct.Description = request.Description;
        foundProduct.Image = request.Image;
        foundProduct.Price = request.Price;
        foundProduct.Amount = request.Amount;

        ProductsDbSet.Update(foundProduct);

        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
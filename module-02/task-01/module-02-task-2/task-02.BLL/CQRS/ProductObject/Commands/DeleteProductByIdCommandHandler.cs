using MediatR;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Context;
using Сommon.DB.Exceptions;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Commands;

public sealed class DeleteProductByIdCommandHandler : BaseProductCommandHandler,
    IRequestHandler<DeleteProductByIdCommand, bool>
{
    public DeleteProductByIdCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var foundProduct =
            await ProductsDbSet.SingleOrDefaultAsync(entity => entity.ProductId == request.ProductId,
                cancellationToken)
            ?? throw new DbRecordNotFoundException(
                $"Can not delete Product. The passed product_id={request.ProductId} not found.");

        if (foundProduct.Amount > request.Amount)
        {
            foundProduct.Amount -= request.Amount;
            ProductsDbSet.Update(foundProduct);
        }
        else
        {
            ProductsDbSet.Remove(foundProduct);
        }

        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0;
    }
}
using MediatR;
using Module_02.Task_02.BLL.CQRS.CategoryObject.Queries;
using Module_02.Task_02.BLL.MappingExtensions;
using Module_02.Task_02.DAL.Context;
using Сommon.DB.Exceptions;

namespace Module_02.Task_02.BLL.CQRS.ProductObject.Commands;

public sealed class CreateProductCommandHandler : BaseProductCommandHandler, IRequestHandler<CreateProductCommand, int>
{
    public CreateProductCommandHandler(IMediator mediator, IWithTrackingStudyDbContext dbContext)
        : base(mediator, dbContext)
    {
    }

    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var foundProduct = ProductsDbSet.FirstOrDefault(entity =>
            entity.Name.Equals(command.Name));

        if (foundProduct != null)
        {
            throw new DbRecordAlreadyExistException($"Can not add a new product with name=\"{command.Name}\". This name is already exist.");
        }

        var isCategoryExist = await Mediator.Send(new CheckCategoryIsExistByIdQuery(command.CategoryId), cancellationToken);
        if (!isCategoryExist)
        {
            throw new DbRecordNotFoundException(
                $"Can not add a new Product. The passed category_id={command.CategoryId} not found.");
        }

        var productEntity = command.ToProductEntity();
        await ProductsDbSet.AddAsync(productEntity, cancellationToken);
        var affectedCount = await DbContext.SaveChangesAsync(cancellationToken);

        return affectedCount > 0
            ? Convert.ToInt32(productEntity.CategoryId)
            : 0;
    }
}
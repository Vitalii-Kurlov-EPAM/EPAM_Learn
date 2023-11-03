namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject.Queries;

public sealed record GetImageByIdQuery(int ImageId) : IQueryRequest<ImageObjectModels.ItemModel>;

using MediatR;
using Module_02.Task_01.CartingService.WebApi.BLL.Models;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Queries;

public sealed class GetImageByIdQuery : IRequest<Image.ItemModel>
{
    public int ImageId { get; }

    public GetImageByIdQuery(int imageId)
    {
        ImageId = imageId;
    }
}
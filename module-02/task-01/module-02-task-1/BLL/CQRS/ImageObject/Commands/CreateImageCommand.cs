using MediatR;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.ImageObject.Commands;

public sealed class CreateImageCommand : IRequest<int>
{
    public string Url { get; set; }
    public string Alt { get; set; }
}
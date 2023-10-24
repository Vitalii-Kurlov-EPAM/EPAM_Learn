using MediatR;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS;

public interface ICommandRequest<out TResponse> : IRequest<TResponse>
{
}
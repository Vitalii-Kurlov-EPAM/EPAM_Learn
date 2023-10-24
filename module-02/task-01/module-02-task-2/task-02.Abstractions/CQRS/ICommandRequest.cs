using MediatR;

namespace Module_02.Task_02.CatalogService.Abstractions.CQRS;

public interface ICommandRequest<out TResponse> : IRequest<TResponse>
{
}
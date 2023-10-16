namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.ImageObject.Commands;

public sealed record CreateImageCommand(string Url, string Alt) : ICommandRequest<int>;

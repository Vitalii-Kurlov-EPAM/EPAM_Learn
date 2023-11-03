using Common.MessageBrokerAbstractions.Interfaces;
using Common.MessageBrokerAbstractions.Params.Errors;

namespace Module_02.Task_02.CatalogService.WebApi.Services;

public class ProducerEventsHandler : IProducerEventsHandler
{
    public Task OnMessageSendErrorAsync(MessageSendErrorParams args)
    {
        Console.WriteLine(args.Error.Message);
        return Task.CompletedTask;
    }

    public Task OnConnectionErrorAsync(ConnectionErrorParams args)
    {
        Console.WriteLine(args.Error.Message);
        return Task.CompletedTask;
    }
}
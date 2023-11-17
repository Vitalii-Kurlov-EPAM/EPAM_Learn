using Common.MessageBrokerAbstractions.MessageHandlers;

namespace Module_02.Task_01.CartingService.WebApi.BackgroundServices;

public record MessageHandlerInfo(IMessageHandler Handler, Type HandlerArgsType, Type MessageType);
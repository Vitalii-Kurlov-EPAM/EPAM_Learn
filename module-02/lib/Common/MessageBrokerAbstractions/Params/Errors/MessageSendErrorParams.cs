namespace Common.MessageBrokerAbstractions.Params.Errors;

public record MessageSendErrorParams(MessageSendParams MessageArguments, Exception Error);
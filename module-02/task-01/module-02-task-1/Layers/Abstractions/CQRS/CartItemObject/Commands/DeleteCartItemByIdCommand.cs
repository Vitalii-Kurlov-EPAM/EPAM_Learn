﻿namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Commands;

public sealed record DeleteCartItemByIdCommand(int CartId, int CartItemId, int Quantity) : ICommandRequest<bool>;

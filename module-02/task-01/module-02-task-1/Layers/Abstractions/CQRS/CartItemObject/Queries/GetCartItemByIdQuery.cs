using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.Common.Queries;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;

public sealed record GetCartItemByIdQuery(string CartId, int CartItemId, bool IncludeDeps = false) : IncludeDepsQuery(IncludeDeps), IQueryRequest<CartItemObjectModels.ItemModel>;
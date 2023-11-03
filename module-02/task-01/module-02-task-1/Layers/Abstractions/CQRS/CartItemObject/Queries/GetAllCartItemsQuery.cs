using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.Common.Queries;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.CQRS.CartItemObject.Queries;

public sealed record GetAllCartItemsQuery(string CartId, bool IncludeDeps = false) : IncludeDepsQuery(IncludeDeps), IQueryRequest<CartItemObjectModels.ItemModel[]>;
﻿using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories.Base;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories;

public interface ICartItemRepository : IBaseCrudRepository<CartItemEntity>, IBaseItemExistRepository
{
    IEnumerable<CartItemEntity> GetAllCartItems(int cartId, bool includeDeps = false);
    CartItemEntity GetCartItem(int cartId, int cartItemId, bool includeDeps = false);
}
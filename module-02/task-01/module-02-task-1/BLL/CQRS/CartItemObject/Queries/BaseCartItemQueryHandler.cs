﻿using MediatR;
using Module_02.Task_01.CartingService.WebApi.DAL.Abstractions;
using Module_02.Task_01.CartingService.WebApi.DAL.DbContext;
using Module_02.Task_01.CartingService.WebApi.DAL.Entities;

namespace Module_02.Task_01.CartingService.WebApi.BLL.CQRS.CartItemObject.Queries;

public abstract class BaseCartItemQueryHandler : BaseHandler<IDbContext>
{
    protected IDbSet<CartItemEntity> CartItemsDbSet => DbContext.CartItems;

    protected BaseCartItemQueryHandler(IMediator mediator, IDbContext dbContext)
        : base(mediator, dbContext)
    {
    }
}
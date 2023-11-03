using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Entities;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories.Base;

namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories;

public interface IImageRepository : IBaseCrudRepository<ImageEntity>, IBaseItemExistRepository
{
}
namespace Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB.Repositories.Base;

public interface IBaseItemExistRepository
{
    bool IsExistById(int id);
}
using FluentValidation;

namespace Module_02.Task_01.CartingService.WebApi.FluentValidations;

public abstract class BaseFluentValidator<TModel> : AbstractValidator<TModel>
{
    protected BaseFluentValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}
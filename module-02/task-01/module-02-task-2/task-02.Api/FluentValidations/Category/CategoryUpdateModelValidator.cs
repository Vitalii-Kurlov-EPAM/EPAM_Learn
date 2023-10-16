using FluentValidation;
using Module_02.Task_02.CatalogService.WebApi.Models.Category;

namespace Module_02.Task_02.CatalogService.WebApi.FluentValidations.Category;

public sealed class CategoryUpdateModelValidator : BaseFluentValidator<CategoryRequest.UpdateModel>
{
    public CategoryUpdateModelValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Required.")
            .MaximumLength(50).WithMessage("Length must be less or equal to 50.");

        RuleFor(p => p.Image)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.Image))
            .WithMessage("Not valid URL.");

        RuleFor(p => p.ParentCategoryId)
            .GreaterThan(0)
            .When(model => model.ParentCategoryId != null)
            .WithMessage("Must be null or greator than zero.");
    }
}
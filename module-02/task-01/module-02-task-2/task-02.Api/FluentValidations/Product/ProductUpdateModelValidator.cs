using FluentValidation;
using Module_02.Task_02.CatalogService.WebApi.Models.Product;

namespace Module_02.Task_02.CatalogService.WebApi.FluentValidations.Product;

public sealed class ProductUpdateModelValidator : BaseFluentValidator<ProductRequest.UpdateModel>
{
    public ProductUpdateModelValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Required.")
            .MaximumLength(50).WithMessage("Length must be less or equal to 50.");

        RuleFor(p => p.Image)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.Image))
            .WithMessage("Not valid URL.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Must be greater than zero.");

        RuleFor(p => p.Amount)
            .GreaterThan(0).WithMessage("Must be greater than zero.");

        RuleFor(p => p.CategoryId)
            .GreaterThan(0).WithMessage("Must be greater than zero.");
    }
}
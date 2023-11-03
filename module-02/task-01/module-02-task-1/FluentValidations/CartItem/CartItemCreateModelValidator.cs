using FluentValidation;
using Module_02.Task_01.CartingService.WebApi.Models.CartItem;

namespace Module_02.Task_01.CartingService.WebApi.FluentValidations.CartItem;

public sealed class CartItemCreateModelValidator : BaseFluentValidator<CartItemRequest.CreateModel>
{
    public CartItemCreateModelValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage("Must be greater than zero.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Required.")
            .MaximumLength(50).WithMessage("Length must be less or equal to 50.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Must be greater than zero.");

        RuleFor(p => p.Quantity)
            .GreaterThan(0).WithMessage("Must be greater than zero.");

        RuleFor(p => p.ImageUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.ImageUrl))
            .WithMessage("Not valid URL.");

        RuleFor(p => p.ImageAlt)
            .MaximumLength(50)
            .When(model => !string.IsNullOrWhiteSpace(model.ImageUrl))
            .WithMessage("Must be null when $.ImageUrl is not presened otherwise the length must be less or equal to 50.");
    }
}
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validates the SaleItem entity according to business rules.
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must be less than 100 characters.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20 units.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

        RuleFor(item => item.Discount)
            .Must((item, discount) =>
            {
                if (item.Quantity < 4)
                    return discount == 0;
                if (item.Quantity >= 4 && item.Quantity < 10)
                    return discount == 0.10m;
                if (item.Quantity >= 10 && item.Quantity <= 20)
                    return discount == 0.20m;
                return false;
            }).WithMessage("Invalid discount based on quantity rules.");

        RuleFor(item => item.Total)
            .GreaterThan(0).WithMessage("Total must be greater than 0.");
    }
}
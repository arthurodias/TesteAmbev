using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validações da entidade SaleItem com base nas regras de negócio.
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(i => i.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(i => i.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

        RuleFor(i => i.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

        RuleFor(i => i)
            .Must(ValidateDiscountRules).WithMessage("Discount rules violated based on quantity.");
    }

    private bool ValidateDiscountRules(SaleItem item)
    {
        if (item.Quantity < 4 && item.Discount != 0) return false;
        if (item.Quantity >= 4 && item.Quantity < 10 && item.Discount != 0.10m) return false;
        if (item.Quantity >= 10 && item.Quantity <= 20 && item.Discount != 0.20m) return false;
        return true;
    }
}
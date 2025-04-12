using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validates the Sale entity according to domain rules.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.")
            .MaximumLength(20).WithMessage("Sale number must be less than 20 characters.");

        RuleFor(sale => sale.Date)
            .NotEmpty().WithMessage("Sale date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.Customer)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100).WithMessage("Customer name must be less than 100 characters.");

        RuleFor(sale => sale.Branch)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(100).WithMessage("Branch name must be less than 100 characters.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());

        RuleFor(sale => sale.Items)
            .Must(items => items != null && items.Count > 0)
            .WithMessage("At least one item is required in a sale.");
    }
}
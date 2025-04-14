using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest, defining rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator class.
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(s => s.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.")
            .Length(3, 20);

        RuleFor(s => s.Customer)
            .NotEmpty().WithMessage("Customer is required.");

        RuleFor(s => s.Branch)
            .NotEmpty().WithMessage("Branch is required.");

        RuleFor(s => s.Items)
            .NotEmpty().WithMessage("At least one item is required.");

        RuleForEach(s => s.Items).SetValidator(new CreateSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator for individual sale items.
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(i => i.Product)
            .NotEmpty().WithMessage("Product is required.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(i => i.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
    }
}
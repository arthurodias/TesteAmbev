using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemDtoValidator());
    }

    public class CreateSaleItemDtoValidator : AbstractValidator<CreateSaleItemDto>
    {
        public CreateSaleItemDtoValidator()
        {
            RuleFor(i => i.ProductName).NotEmpty().MaximumLength(100);
            RuleFor(i => i.Quantity).GreaterThan(0);
            RuleFor(i => i.UnitPrice).GreaterThan(0);
        }
    }
}
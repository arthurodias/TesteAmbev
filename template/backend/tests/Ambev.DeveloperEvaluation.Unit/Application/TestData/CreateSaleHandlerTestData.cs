using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides test data for CreateSaleHandler
/// </summary>
public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> createSaleFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
        .RuleFor(s => s.SaleDate, f => f.Date.Recent())
        .RuleFor(s => s.Customer, f => f.Company.CompanyName())
        .RuleFor(s => s.Branch, f => f.Commerce.Department())
        .RuleFor(s => s.Items, f => new List<CreateSaleItemDto>
        {
            new CreateSaleItemDto
            {
                ProductName = f.Commerce.ProductName(),
                UnitPrice = f.Random.Decimal(10, 500),
                Quantity = f.Random.Int(1, 10)
            }
        });

    public static CreateSaleCommand GenerateValidCommand() => createSaleFaker.Generate();
}
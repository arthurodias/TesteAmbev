using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 100))
        .RuleFor(i => i.IsCancelled, _ => false);

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
        .RuleFor(s => s.SaleDate, f => f.Date.Past())
        .RuleFor(s => s.Customer, f => f.Person.FullName)
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(s => s.IsCancelled, _ => false)
        .RuleFor(s => s.Items, f => SaleItemFaker.Generate(3));

    public static Sale GenerateValidSale() => SaleFaker.Generate();

    public static CreateSaleCommand GenerateValidCreateSaleCommand()
    {
        var faker = new Faker();
        return new CreateSaleCommand
        {
            SaleNumber = faker.Random.AlphaNumeric(10),
            SaleDate = faker.Date.Past(),
            Customer = faker.Person.FullName,
            Branch = faker.Company.CompanyName(),
            Items = new List<CreateSaleItemDto>
            {
                new()
                {
                    ProductName = faker.Commerce.ProductName(),
                    Quantity = 2,
                    UnitPrice = 50
                }
            }
        };
    }
}
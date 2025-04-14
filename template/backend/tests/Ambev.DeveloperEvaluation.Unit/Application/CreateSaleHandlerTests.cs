using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NSubstitute;
using Xunit;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _handler = new CreateSaleHandler(_saleRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldCreateSale_WhenCommandIsValid()
    {
        // Arrange
        var command = new Faker<CreateSaleCommand>()
            .RuleFor(c => c.SaleNumber, f => f.Commerce.Ean13())
            .RuleFor(c => c.SaleDate, f => f.Date.Past())
            .RuleFor(c => c.Customer, f => f.Person.FullName)
            .RuleFor(c => c.Branch, f => f.Company.CompanyName())
            .RuleFor(c => c.Items, f => new List<CreateSaleItemDto>
            {
                new CreateSaleItemDto
                {
                    ProductName = f.Commerce.ProductName(),
                    UnitPrice = f.Random.Decimal(1, 100),
                    Quantity = f.Random.Int(1, 10)
                }
            })
            .Generate();

        var sale = new Sale
        {
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            Customer = command.Customer,
            Branch = command.Branch
        };

        command.Items.ForEach(item => sale.Items.Add(new SaleItem
        {
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice
        }));

        _mapper.Map<Sale>(command).Returns(sale);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        await _saleRepository.Received(1).CreateAsync(Arg.Is<Sale>(s =>
            s.SaleNumber == command.SaleNumber &&
            s.Customer == command.Customer &&
            s.Branch == command.Branch &&
            s.SaleDate == command.SaleDate &&
            s.Items.Count == command.Items.Count));

        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
    }
}
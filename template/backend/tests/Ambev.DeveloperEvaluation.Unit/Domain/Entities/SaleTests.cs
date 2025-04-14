using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact(DisplayName = "Validate should return valid when sale data is correct")]
    public void Given_ValidSale_When_Validated_Then_ShouldBeValid()
    {
        var sale = SaleTestData.GenerateValidSale();

        var result = sale.Validate();

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "Validate should return invalid when sale data is incorrect")]
    public void Given_InvalidSale_When_Validated_Then_ShouldBeInvalid()
    {
        var sale = new Sale
        {
            SaleNumber = "", // inválido
            Customer = "",   // inválido
            Branch = "",     // inválido
            Items = new List<SaleItem>() // nenhum item
        };

        var result = sale.Validate();

        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Cancel should mark sale and all items as cancelled")]
    public void Given_Sale_When_Cancelled_Then_ShouldSetIsCancelledTrueAndCancelItems()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        sale.Cancel();

        // Assert
        sale.IsCancelled.Should().BeTrue();
        sale.Items.Should().OnlyContain(i => i.IsCancelled);
    }

    [Fact(DisplayName = "Should apply no discount when quantity is less than 4")]
    public void Given_LessThan4Items_When_CalculatingDiscount_Then_ShouldNotApplyDiscount()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 3,  // Menor que 4
            UnitPrice = 50
        };

        // Act
        var discount = saleItem.Discount;

        // Assert
        discount.Should().Be(0m);  // Nenhum desconto deve ser aplicado
    }

    [Fact(DisplayName = "Should apply 10% discount when quantity is between 4 and 9")]
    public void Given_4To9Items_When_CalculatingDiscount_Then_ShouldApply10PercentDiscount()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 6,  // Entre 4 e 9
            UnitPrice = 50
        };

        // Act
        var discount = saleItem.Discount;

        // Assert
        discount.Should().Be(0.1m * 6 * 50);  // Espera-se 10% de desconto sobre o valor total
    }

    [Fact(DisplayName = "Should apply 20% discount when quantity is between 10 and 20")]
    public void Given_10To20Items_When_CalculatingDiscount_Then_ShouldApply20PercentDiscount()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 15,  // Entre 10 e 20
            UnitPrice = 50
        };

        // Act
        var discount = saleItem.Discount;

        // Assert
        discount.Should().Be(0.2m * 15 * 50);  // Espera-se 20% de desconto sobre o valor total
    }

    [Fact(DisplayName = "Should not allow selling more than 20 identical items")]
    public void Given_MoreThan20Items_When_CalculatingDiscount_Then_ShouldNotAllowSale()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 25,  // Mais de 20 itens
            UnitPrice = 50
        };

        // Act
        Assert.Throws<InvalidOperationException>(() => saleItem.Discount);  // Deve lançar exceção, pois não pode vender mais de 20 itens
    }
}
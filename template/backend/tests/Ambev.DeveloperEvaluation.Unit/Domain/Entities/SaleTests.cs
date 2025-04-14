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
}
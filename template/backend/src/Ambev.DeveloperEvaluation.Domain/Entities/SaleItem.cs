using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product item within a sale, including pricing, quantity, and discount logic.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the external product ID (denormalized).
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name (denormalized for reporting).
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets whether the item was cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the discount applied to this item.
    /// </summary>
    public decimal Discount  => CalculateDiscount();

    /// <summary>
    /// Gets the total amount for this item (after discount).
    /// </summary>
    public decimal TotalAmount => Quantity * UnitPrice - Discount;

    /// <summary>
    /// Performs validation of the SaleItem entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>A ValidationResultDetail indicating whether the item is valid.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    /// <summary>
    /// Cancels this item.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }

    /// <summary>
    /// Calculates the discount based on business rules.
    /// </summary>
    /// <returns>Discount value for the item.</returns>
    private decimal CalculateDiscount()
    {
        if (Quantity < 4) return 0m;
        if (Quantity >= 10 && Quantity <= 20) return 0.2m * Quantity * UnitPrice;
        if (Quantity >= 4 && Quantity < 10) return 0.1m * Quantity * UnitPrice;
        return 0m;
    }
}
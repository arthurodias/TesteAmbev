using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a single item in a sale, including quantity, price, and discounts.
/// </summary>
public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }
    public Sale? Sale { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; } // percentage, e.g., 0.10 or 0.20

    /// <summary>
    /// Total amount for the item after discount.
    /// </summary>
    public decimal Total => Quantity * UnitPrice * (1 - Discount);

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the SaleItem class with current UTC timestamp.
    /// </summary>
    public SaleItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates the current SaleItem entity using SaleItemValidator rules.
    /// </summary>
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
}
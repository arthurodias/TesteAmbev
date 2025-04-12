using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sales transaction containing items, customer, branch, and status information.
/// </summary>
public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Gets the total amount of the sale, calculated from item totals.
    /// </summary>
    public decimal TotalAmount => Items.Sum(item => item.Total);

    /// <summary>
    /// Initializes a new instance of the Sale class with current UTC timestamp.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels the sale.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates the current Sale entity using SaleValidator rules.
    /// </summary>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Sale identification number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The name of the customer making the purchase.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// The branch where the sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Items included in the sale.
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Represents an item in a sale request.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Product name.
    /// </summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
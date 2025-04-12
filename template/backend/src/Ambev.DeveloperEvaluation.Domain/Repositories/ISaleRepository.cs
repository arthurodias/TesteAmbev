using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Defines operations for interacting with Sale entities in the data store.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Gets a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The sale's unique identifier.</param>
    /// <returns>The matching sale if found; otherwise, null.</returns>
    Task<Sale?> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets all sales in the system.
    /// </summary>
    /// <returns>A collection of all sales.</returns>
    Task<IEnumerable<Sale>> GetAllAsync();

    /// <summary>
    /// Adds a new sale to the data store.
    /// </summary>
    /// <param name="sale">The sale entity to add.</param>
    Task AddAsync(Sale sale);

    /// <summary>
    /// Updates an existing sale in the data store.
    /// </summary>
    /// <param name="sale">The sale entity to update.</param>
    Task UpdateAsync(Sale sale);

    /// <summary>
    /// Removes a sale from the data store.
    /// </summary>
    /// <param name="sale">The sale entity to remove.</param>
    Task RemoveAsync(Sale sale);

    /// <summary>
    /// Persists changes to the data store.
    /// </summary>
    /// <returns>True if changes were saved successfully; otherwise, false.</returns>
    Task<bool> SaveChangesAsync();
}
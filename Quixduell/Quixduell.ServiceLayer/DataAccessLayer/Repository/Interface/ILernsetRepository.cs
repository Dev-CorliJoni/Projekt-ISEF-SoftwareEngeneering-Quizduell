using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface
{
    public interface ILernsetRepository
    {
        /// <summary>
        /// Creates a new Lernset.
        /// </summary>
        /// <param name="lernset">The Lernset to create.</param>
        Task CreateLernsetAsync(Lernset lernset);

        /// <summary>
        /// Searches for a Lernset by its ID.
        /// </summary>
        /// <param name="id">The ID of the Lernset to search for.</param>
        /// <returns>The found Lernset or null if not found.</returns>
        Task<Lernset?> GetLernsetByIDAsync(Guid id);

        /// <summary>
        /// Retrieves all existing Lernsets.
        /// </summary>
        /// <returns>A list of all existing Lernsets.</returns>
        Task<IEnumerable<Lernset>> GetLernsetsAsync();

        /// <summary>
        /// Updates an existing Lernset.
        /// </summary>
        /// <param name="lernset">The Lernset to update.</param>
        Task UpdateLernsetAsync(Lernset lernset);

        /// <summary>
        /// Deletes a Lernset by its ID.
        /// </summary>
        /// <param name="id">The ID of the Lernset to delete.</param>
        Task DeleteLernsetAsync(Guid id);
    }
}

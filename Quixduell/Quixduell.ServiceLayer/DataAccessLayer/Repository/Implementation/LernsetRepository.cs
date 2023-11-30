using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Interface;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException;
namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    /// <summary>
    /// Implementation of the Lernset Repository interface that enables database operations for Lernsets.
    /// </summary>
    internal class LernsetRepository : ILernsetRepository
    {
        private readonly AppDatabaseContext<AppUser> _appDatabaseContext; // Database context

        /// <summary>
        /// Constructor for the Lernset Repository.
        /// </summary>
        /// <param name="appDatabaseContext">The database context for the application.</param>
        public LernsetRepository(AppDatabaseContext<AppUser> appDatabaseContext)
        {
            _appDatabaseContext = appDatabaseContext; // Initializing the database context
        }

        /// <summary>
        /// Retrieves all existing Lernsets.
        /// </summary>
        /// <returns>A list of all existing Lernsets.</returns>
        public async Task<IEnumerable<Lernset>> GetLernsetsAsync()
        {
            return await _appDatabaseContext.Lernsets
                .Include(o => o.Creator)
                .Include(o => o.Contributors)
                .Include(o => o.Category)
                .Include(o => o.Questions)
                .ToListAsync();
        }

        /// <summary>
        /// Searches for a Lernset by its ID.
        /// </summary>
        /// <param name="id">The ID of the Lernset to search for.</param>
        /// <returns>The found Lernset or null if not found.</returns>
        public async Task<Lernset?> GetLernsetByIDAsync(Guid id)
        {
            var lernset = await _appDatabaseContext.Lernsets
                .Include(o => o.Creator)
                .Include(o => o.Contributors)
                .Include(o => o.Category)
                .Include(o => o.Questions)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (lernset is not null)
            {
                return lernset;
            }

            return null;
        }

        /// <summary>
        /// Creates a new Lernset.
        /// </summary>
        /// <param name="lernset">The Lernset to create.</param>
        public async Task CreateLernsetAsync(Lernset lernset)
        {
            await _appDatabaseContext.Lernsets.AddAsync(lernset);
            await _appDatabaseContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing Lernset.
        /// </summary>
        /// <param name="lernset">The Lernset to update.</param>
        public async Task UpdateLernsetAsync(Lernset lernset)
        {
            var obj = await GetLernsetByIDAsync(lernset.ID);

            if (obj is null)
            {
                throw new LernsetNotFoundException(lernset.ID);
            }
            obj.Category = lernset.Category;
            obj.Contributors = lernset.Contributors;
            obj.Questions = lernset.Questions;

            await _appDatabaseContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a Lernset by its ID.
        /// </summary>
        /// <param name="id">The ID of the Lernset to delete.</param>
        public async Task DeleteLernsetAsync(Guid id)
        {
            Lernset? obj = null;
            try
            {
                obj = await GetLernsetByIDAsync(id);
            }
            catch
            {

            }

            if (obj is not null)
            {
                _appDatabaseContext.Lernsets.Remove(obj);
                await _appDatabaseContext.SaveChangesAsync();
            }
        }
    }
}

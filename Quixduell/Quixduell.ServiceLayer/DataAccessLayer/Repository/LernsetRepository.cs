using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository
{
    internal class LernsetRepository : ILernsetRepository
    {
        private readonly AppDatabaseContext<AppUser> _appDatabaseContext;

        public LernsetRepository(AppDatabaseContext<AppUser> appDatabaseContext)
        {
            _appDatabaseContext = appDatabaseContext;
        }

        public async Task<IEnumerable<Lernset>> GetLernsetsAsync()
        {
            return await _appDatabaseContext.Lernsets
                .Include(o => o.Creator)
                .Include(o => o.Contributors)
                .Include(o => o.Category)
                .Include(o => o.Questions)
                .ToListAsync();
        }

        public async Task<Lernset> GetLernsetByIDAsync(Guid id)
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

            throw new NotImplementedException();
        }

        public async Task CreateLernsetAsync(Lernset lernset)
        {
            await _appDatabaseContext.Lernsets.AddAsync(lernset);
            await _appDatabaseContext.SaveChangesAsync();
        }

        public async Task UpdateLernsetAsync(Lernset lernset)
        {
            var obj = await GetLernsetByIDAsync(lernset.ID);

            obj.Category = lernset.Category;
            obj.Contributors = lernset.Contributors;
            obj.Questions = lernset.Questions;
        }


    }
}

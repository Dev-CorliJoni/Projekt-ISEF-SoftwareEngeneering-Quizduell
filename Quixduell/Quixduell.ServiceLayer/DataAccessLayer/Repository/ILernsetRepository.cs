using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository
{
    internal interface ILernsetRepository
    {
        Task CreateLernsetAsync(Lernset lernset);
        Task<Lernset> GetLernsetByIDAsync(Guid id);
        Task<IEnumerable<Lernset>> GetLernsetsAsync();
        Task UpdateLernsetAsync(Lernset lernset);
    }
}
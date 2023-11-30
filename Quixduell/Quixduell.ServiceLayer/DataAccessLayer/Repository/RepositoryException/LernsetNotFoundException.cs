namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException
{
    internal class LernsetNotFoundException : Exception 
    {
        public LernsetNotFoundException(Guid id) : base($"Lernset with id: {id} not found")
        {
            
        }
    }
}

namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException
{
    internal class StudysetNotFoundException : Exception 
    {
        public StudysetNotFoundException(Guid id) : base($"Studyset with id: {id} not found")
        {
            
        }
    }
}

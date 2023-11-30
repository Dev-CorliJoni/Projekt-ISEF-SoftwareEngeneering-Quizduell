namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.RepositoryException
{
    internal class CategoryNotFoundException : Exception 
    {
        public CategoryNotFoundException(Guid id) : base($"Category with id: {id} not found")
        {
            
        }
    }
}

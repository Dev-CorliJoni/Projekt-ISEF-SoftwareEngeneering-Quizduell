namespace Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation
{
    /// <summary>
    /// Implementation of the Studyset Repository interface that enables database operations for Studysets.
    /// </summary>
    internal class StudysetRepository 
    {
        //private readonly AppDatabaseContext<User> _appDatabaseContext; // Database context

        ///// <summary>
        ///// Constructor for the Studyset Repository.
        ///// </summary>
        ///// <param name="appDatabaseContext">The database context for the application.</param>
        //public StudysetRepository(AppDatabaseContext<User> appDatabaseContext)
        //{
        //    _appDatabaseContext = appDatabaseContext; // Initializing the database context
        //}

        ///// <summary>
        ///// Retrieves all existing Studysets.
        ///// </summary>
        ///// <returns>A list of all existing Studysets.</returns>
        //public async Task<IEnumerable<Studyset>> GetStudysetsAsync()
        //{
        //    return await _appDatabaseContext.Studysets
        //        .Include(o => o.Creator)
        //        .Include(o => o.Contributors)
        //        .Include(o => o.Category)
        //        .Include(o => o.Questions)
        //        .ToListAsync();
        //}

        ///// <summary>
        ///// Searches for a Studyset by its ID.
        ///// </summary>
        ///// <param name="id">The ID of the Studyset to search for.</param>
        ///// <returns>The found Studyset or null if not found.</returns>
        //public async Task<Studyset?> GetStudysetByIDAsync(Guid id)
        //{
        //    var studySet = await _appDatabaseContext.Studysets
        //        .Include(o => o.Creator)
        //        .Include(o => o.Contributors)
        //        .Include(o => o.Category)
        //        .Include(o => o.Questions)
        //        .FirstOrDefaultAsync(o => o.ID == id);

        //    if (studySet is not null)
        //    {
        //        return studySet;
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// Creates a new Studyset.
        ///// </summary>
        ///// <param name="studyset">The Studyset to create.</param>
        //public async Task CreateStudysetAsync(Studyset studyset)
        //{
        //    await _appDatabaseContext.Studysets.AddAsync(studyset);
        //    await _appDatabaseContext.SaveChangesAsync();
        //}

        ///// <summary>
        ///// Updates an existing Studyset.
        ///// </summary>
        ///// <param name="studyset">The Studyset to update.</param>
        //public async Task UpdateStudysetAsync(Studyset studyset)
        //{
        //    var obj = await GetStudysetByIDAsync(studyset.ID);

        //    if (obj is null)
        //    {
        //        throw new StudysetNotFoundException(studyset.ID);
        //    }
        //    obj.Category = studyset.Category;
        //    obj.Contributors = studyset.Contributors;
        //    obj.Questions = studyset.Questions;

        //    await _appDatabaseContext.SaveChangesAsync();
        //}

        ///// <summary>
        ///// Deletes a Studyset by its ID.
        ///// </summary>
        ///// <param name="id">The ID of the Studyset to delete.</param>
        //public async Task DeleteStudysetAsync(Guid id)
        //{
        //    Studyset? obj = null;
        //    try
        //    {
        //        obj = await GetStudysetByIDAsync(id);
        //    }
        //    catch
        //    {

        //    }

        //    if (obj is not null)
        //    {
        //        _appDatabaseContext.Studysets.Remove(obj);
        //        await _appDatabaseContext.SaveChangesAsync();
        //    }
        //}
    }
}

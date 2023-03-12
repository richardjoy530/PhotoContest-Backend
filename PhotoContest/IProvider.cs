#region

using System.Collections.Generic;

#endregion

namespace PhotoContest;

/// <summary>
///     An interface for all the basic data access operations
/// </summary>
public interface IProvider<TModel> where TModel : IDataRecord
{
    /// <summary>
    ///     Retrieves the <typeparamref name="TModel" /> with the given <paramref name="id" /> from the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    ///     <typeparamref name="TModel" />
    /// </returns>
    public TModel GetById(int id);

    /// <summary>
    ///     Retrieves a list of all the <typeparamref name="TModel" /> in database
    /// </summary>
    /// <returns>An IEnumerable of <typeparamref name="TModel" /></returns>
    public IEnumerable<TModel> GetAll();

    /// <summary>
    ///     Creates a new <typeparamref name="TModel" /> in the database
    /// </summary>
    /// <param name="data"></param>
    /// <returns>
    ///     Id of the inserted <typeparamref name="TModel" />
    /// </returns>
    public int Insert(TModel data);

    /// <summary>
    ///     Updates <typeparamref name="TModel" /> by <paramref name="id" />
    /// </summary>
    /// <param name="data"></param>
    /// <param name="id"></param>
    public void Update(TModel data, int id);

    /// <summary>
    ///     Deletes the record of <typeparamref name="TModel" /> with <paramref name="id" />
    /// </summary>
    /// <param name="id"></param>
    public bool Delete(int id);
}
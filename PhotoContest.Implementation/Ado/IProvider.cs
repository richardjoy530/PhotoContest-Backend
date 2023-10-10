using System.Collections.Generic;

namespace PhotoContest.Implementation.Ado
{
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
        ///     Retrieves all the Id in database.
        /// </summary>
        public int[] GetAllIds();

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
        ///     Updates <typeparamref name="TModel" />
        /// </summary>
        /// <param name="data"></param>
        /// <param name="updateParams"></param>
        public bool Update(TModel data, long updateParams);

        /// <summary>
        ///     Deletes the record of <typeparamref name="TModel" /> with <paramref name="id" />
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(int id);
    }
}
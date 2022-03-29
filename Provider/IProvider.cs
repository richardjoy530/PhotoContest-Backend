using System.Collections.Generic;

namespace Provider
{
    /// <summary>
    /// An interface for all the basic data access operations
    /// </summary>
    public interface IProvider<TModel> where TModel : class
    {
        /// <summary>
        /// Retreves the <typeparamref name="TModel"/> with the given <paramref name="referenceId"/> from the database
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns><typeparamref name="TModel"/></returns>
        public TModel GetById(string referenceId);

        /// <summary>
        /// Retreves a list of all the <typeparamref name="TModel"/> in database
        /// </summary>
        /// <returns>An IEnumerable of <typeparamref name="TModel"/></returns>
        public IEnumerable<TModel> GetAll();

        /// <summary>
        /// Creates a new <typeparamref name="TModel"/> in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns><typeparamref name="TModel"/></returns>
        public TModel Create(TModel model);

        /// <summary>
        /// Updates <typeparamref name="TModel"/> by <paramref name="referenceId"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="referenceId"></param>
        /// <returns><typeparamref name="TModel"/></returns>
        public TModel Update(TModel model, string referenceId);

        /// <summary>
        /// Deletes the record of <typeparamref name="TModel"/> with <paramref name="referenceId"/>
        /// </summary>
        /// <param name="referenceId"></param>
        public void Delete(string referenceId);
    }
}

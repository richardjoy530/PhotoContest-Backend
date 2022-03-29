using Provider.Models;
using System.Collections.Generic;

namespace Provider
{
    /// <summary>
    /// An interface for fetching <see cref="Photographer"/> recored
    /// </summary>
    public interface IPhotographerProvider
    {
        /// <summary>
        /// Retreves the <see cref="Photographer"/> with the given id
        /// </summary>
        /// <param name="id"/>
        /// <returns><see cref="Photographer"/></returns>
        public Photographer GetPhotographer(int id);

        /// <summary>
        /// Retreves a list of all the <see cref="Photographer"/> in database
        /// </summary>
        /// <returns>Enumerable of <see cref="Photographer"/></returns>
        public IEnumerable<Photographer> GetPhotographers();

        /// <summary>
        /// Creates a new <see cref="Photographer"/> in the database
        /// </summary>
        /// <param name="photographer"></param>
        /// <returns>Created <see cref="Photographer"/> from the database</returns>
        public Photographer AddPhotographer(Photographer photographer);

        /// <summary>
        /// Deletes the record of <see cref="PhotoEntry"/> with the specified ID
        /// </summary>
        /// <remarks>Do not use this until its absolutely necessary. Since it can mess up the id mappings of other records</remarks>
        /// <param name="id"></param>
        public void DeletePhotographer(int id);
    }
}

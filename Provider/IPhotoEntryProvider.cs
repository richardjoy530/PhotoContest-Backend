using Provider.Models;
using System.Collections.Generic;

namespace Provider
{
    /// <summary>
    /// An interface for fetching photo entry records
    /// </summary>
    public interface IPhotoEntryProvider
    {
        /// <summary>
        /// Retreves a list of all the <see cref="PhotoEntry"/> in database
        /// </summary>
        /// <returns>Enumerable of <see cref="PhotoEntry"/></returns>
        public IEnumerable<PhotoEntry> GetPhotoEntries();

        /// <summary>
        /// Retreves a list of all the <see cref="PhotoEntry"/> in database with the given theme
        /// </summary>
        /// <param name="theme"/>
        /// <returns>Enumerable of <see cref="PhotoEntry"/></returns>
        public IEnumerable<PhotoEntry> GetPhotoEntries(string theme);

        /// <summary>
        /// Creates a new <see cref="PhotoEntry"/> in the database
        /// </summary>
        /// <param name="photoEntry"></param>
        /// <returns>Created <see cref="PhotoEntry"/> from the database</returns>
        public PhotoEntry AddPhotoEntry(PhotoEntry photoEntry);

        /// <summary>
        /// Updates <see cref="PhotoEntry"/> by ID
        /// </summary>
        /// <param name="photoEntry"></param>
        /// <returns>Updated <see cref="PhotoEntry"/> from the database</returns>
        public PhotoEntry UpdatePhotoEntry(PhotoEntry photoEntry);

        /// <summary>
        /// Deletes the record of <see cref="PhotoEntry"/> with the specified ID
        /// </summary>
        /// <param name="referenceId"></param>
        public void DeletePhotoEntry(int id);
    }
}

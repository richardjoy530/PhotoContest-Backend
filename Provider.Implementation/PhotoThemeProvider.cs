using Provider.Models;
using System;
using System.Collections.Generic;

namespace Provider.Implementation
{
    /// <summary>
    /// Database access layer of <see cref="PhotoTheme"/>
    /// </summary>
    public class PhotoThemeProvider : IProvider<PhotoTheme>
    {
        /// <inheritdoc/>
        public PhotoTheme Insert(PhotoTheme model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(string referenceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<PhotoTheme> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PhotoTheme GetById(string referenceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PhotoTheme Update(PhotoTheme model, string referenceId)
        {
            throw new NotImplementedException();
        }
    }
}

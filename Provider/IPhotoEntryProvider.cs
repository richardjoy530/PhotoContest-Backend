using Provider.Models;
using System;
using System.Collections.Generic;

namespace Provider
{
    public interface IPhotoEntryProvider
    {
        public IEnumerable<PhotoEntry> GetPhotoEntries();

        public IEnumerable<PhotoEntry> GetPhotoEntries(string theme);

        public PhotoEntry AddPhotoEntry();

        public PhotoEntry UpdatePhotoEntry();

        public void DeletePhotoEntry();
    }
}

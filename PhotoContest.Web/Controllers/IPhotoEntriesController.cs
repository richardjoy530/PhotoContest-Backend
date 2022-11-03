using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;
using System.Collections.Generic;

namespace PhotoContest.Web.Controllers
{
    public interface IPhotoEntriesController
    {
        PhotoEntry CreatePhotoEntry([FromBody] PhotoEntry photoEntry);
        void Delete(string referenceId);
        IEnumerable<PhotoEntry> GetAll();
        IEnumerable<PhotoEntry> GetAllByTheme(string theme);
        PhotoEntry GetById(string referenceId);
        PhotoEntry UpdatePhotoEntry(string referenceId, [FromBody] PhotoEntry photoEntry);
    }
}
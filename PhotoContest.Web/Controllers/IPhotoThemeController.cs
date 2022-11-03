using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

namespace PhotoContest.Web.Controllers
{
    public interface IPhotoThemeController
    {
        PhotoTheme CreatePhotographer([FromBody] PhotoTheme photoTheme);
        void Delete(string referenceId);
        System.Collections.Generic.IEnumerable<PhotoTheme> GetAll();
        PhotoTheme GetById(string referenceId);
        PhotoTheme UpdatePhotographer(string referenceId, [FromBody] PhotoTheme photoTheme);
    }
}
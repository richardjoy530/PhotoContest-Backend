using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

namespace PhotoContest.Web.Controllers
{
    public interface IPhotographerController
    {
        Photographer CreatePhotographer([FromBody] Photographer photographer);
        void Delete(string referenceId);
        System.Collections.Generic.IEnumerable<Photographer> GetAll();
        Photographer GetById(string referenceId);
        Photographer UpdatePhotographer(string referenceId, [FromBody] Photographer photographer);
    }
}
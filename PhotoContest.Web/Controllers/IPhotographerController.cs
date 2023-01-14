#region

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Controllers;

/// <summary>
/// </summary>
public interface IPhotographerController
{
    /// <summary>
    /// </summary>
    /// <param name="photographer"></param>
    /// <returns></returns>
    Photographer CreatePhotographer([FromBody] Photographer photographer);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    void Delete(string referenceId);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    IEnumerable<Photographer> GetAll();

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    Photographer GetById(string referenceId);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="photographer"></param>
    /// <returns></returns>
    Photographer UpdatePhotographer(string referenceId, [FromBody] Photographer photographer);
}
#region

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Controllers;

/// <summary>
/// </summary>
public interface IPhotoThemeController
{
    /// <summary>
    /// </summary>
    /// <param name="contest"></param>
    /// <returns></returns>
    Contest CreatePhotographer([FromBody] Contest contest);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    void Delete(string referenceId);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    IEnumerable<Contest> GetAll();

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    Contest GetById(string referenceId);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="contest"></param>
    /// <returns></returns>
    Contest UpdatePhotographer(string referenceId, [FromBody] Contest contest);
}
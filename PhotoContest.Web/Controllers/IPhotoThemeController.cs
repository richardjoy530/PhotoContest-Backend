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
    /// <param name="photoTheme"></param>
    /// <returns></returns>
    PhotoTheme CreatePhotographer([FromBody] PhotoTheme photoTheme);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    void Delete(string referenceId);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    IEnumerable<PhotoTheme> GetAll();

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    PhotoTheme GetById(string referenceId);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="photoTheme"></param>
    /// <returns></returns>
    PhotoTheme UpdatePhotographer(string referenceId, [FromBody] PhotoTheme photoTheme);
}
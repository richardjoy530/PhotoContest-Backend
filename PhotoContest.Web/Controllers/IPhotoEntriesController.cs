#region

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Controllers;

/// <summary>
/// </summary>
public interface IPhotoEntriesController
{
    /// <summary>
    /// </summary>
    /// <param name="photoEntry"></param>
    /// <returns></returns>
    PhotoEntry CreatePhotoEntry([FromBody] PhotoEntry photoEntry);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    void Delete(string referenceId);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    IEnumerable<PhotoEntry> GetAll();

    /// <summary>
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    IEnumerable<PhotoEntry> GetAllByTheme(string theme);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    PhotoEntry GetById(string referenceId);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="photoEntry"></param>
    /// <returns></returns>
    PhotoEntry UpdatePhotoEntry(string referenceId, [FromBody] PhotoEntry photoEntry);
}
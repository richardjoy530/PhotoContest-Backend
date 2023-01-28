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
    /// <param name="submission"></param>
    /// <returns></returns>
    Submission CreatePhotoEntry([FromBody] Submission submission);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    void Delete(string referenceId);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    IEnumerable<Submission> GetAll();

    /// <summary>
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    IEnumerable<Submission> GetAllByTheme(string theme);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    Submission GetById(string referenceId);

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="submission"></param>
    /// <returns></returns>
    Submission UpdatePhotoEntry(string referenceId, [FromBody] Submission submission);
}
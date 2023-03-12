#region

using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Controllers;

/// <summary>
/// </summary>
public interface IContestController
{
    /// <summary>
    /// </summary>
    /// <param name="contest"></param>
    /// <returns></returns>
    int CreateContest([FromBody] Contest contest);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    Contest[] GetAll();
}
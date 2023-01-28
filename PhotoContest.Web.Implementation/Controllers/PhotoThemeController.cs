#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Models;
using PhotoContest.Web.Controllers;
using PhotoContest.Web.Implementation.Converters;

#endregion

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
///     PhotoThemeController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PhotoThemeController : ControllerBase, IPhotoThemeController
{
    private readonly IProvider<Contest> _photoThemeProvider;

    /// <summary>
    /// </summary>
    /// <param name="photoThemeProvider"></param>
    public PhotoThemeController(IProvider<Contest> photoThemeProvider)
    {
        this._photoThemeProvider = photoThemeProvider ?? throw new ArgumentNullException(nameof(photoThemeProvider));
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    [HttpGet("{referenceId}")]
    public Contracts.Contest GetById(string referenceId)
    {
        return _photoThemeProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<Contracts.Contest> GetAll()
    {
        return _photoThemeProvider.GetAll().ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="contest"></param>
    /// <returns></returns>
    [HttpPost]
    public Contracts.Contest CreatePhotographer([FromBody] Contracts.Contest contest)
    {
        if (string.IsNullOrWhiteSpace(contest.ReferenceId))
            contest.ReferenceId = Guid.NewGuid().ToString();
        else if (!Guid.TryParse(contest.ReferenceId, out _))
            throw new ValidationException($"Invalid {nameof(contest.ReferenceId)}");

        return _photoThemeProvider.Insert(contest.ToModel()).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="contest"></param>
    /// <returns></returns>
    [HttpPut("{referenceId}")]
    public Contracts.Contest UpdatePhotographer(string referenceId, [FromBody] Contracts.Contest contest)
    {
        if (referenceId != contest.ReferenceId)
            throw new ValidationException($"{nameof(contest.ReferenceId)} does not match within the request");

        _photoThemeProvider.Update(contest.ToModel(), referenceId);
        return _photoThemeProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    [HttpDelete("{referenceId}")]
    public void Delete(string referenceId)
    {
        _photoThemeProvider.Delete(referenceId);
    }
}
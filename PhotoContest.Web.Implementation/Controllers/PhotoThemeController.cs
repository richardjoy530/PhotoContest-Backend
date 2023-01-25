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
    private readonly IProvider<PhotoTheme> _photoThemeProvider;

    /// <summary>
    /// </summary>
    /// <param name="photoThemeProvider"></param>
    public PhotoThemeController(IProvider<PhotoTheme> photoThemeProvider)
    {
        this._photoThemeProvider = photoThemeProvider ?? throw new ArgumentNullException(nameof(photoThemeProvider));
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    [HttpGet("{referenceId}")]
    public Contracts.PhotoTheme GetById(string referenceId)
    {
        return _photoThemeProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<Contracts.PhotoTheme> GetAll()
    {
        return _photoThemeProvider.GetAll().ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="photoTheme"></param>
    /// <returns></returns>
    [HttpPost]
    public Contracts.PhotoTheme CreatePhotographer([FromBody] Contracts.PhotoTheme photoTheme)
    {
        if (string.IsNullOrWhiteSpace(photoTheme.ReferenceId))
            photoTheme.ReferenceId = Guid.NewGuid().ToString();
        else if (!Guid.TryParse(photoTheme.ReferenceId, out _))
            throw new ValidationException($"Invalid {nameof(photoTheme.ReferenceId)}");

        return _photoThemeProvider.Insert(photoTheme.ToModel()).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="photoTheme"></param>
    /// <returns></returns>
    [HttpPut("{referenceId}")]
    public Contracts.PhotoTheme UpdatePhotographer(string referenceId, [FromBody] Contracts.PhotoTheme photoTheme)
    {
        if (referenceId != photoTheme.ReferenceId)
            throw new ValidationException($"{nameof(photoTheme.ReferenceId)} does not match within the request");

        _photoThemeProvider.Update(photoTheme.ToModel(), referenceId);
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
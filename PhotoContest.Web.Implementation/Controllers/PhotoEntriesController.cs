#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Models;
using PhotoContest.Web.Controllers;
using PhotoContest.Web.Implementation.Converters;

#endregion

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
///     Submission Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PhotoEntriesController : ControllerBase, IPhotoEntriesController
{
    private readonly IProvider<Submission> _photoEntryProvider;

    /// <summary>
    ///     Initialises a Submission Controller
    /// </summary>
    /// <param name="photoEntryProvider"></param>
    public PhotoEntriesController(IProvider<Submission> photoEntryProvider)
    {
        _photoEntryProvider = photoEntryProvider ?? throw new ArgumentNullException(nameof(photoEntryProvider));
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet("{referenceId}")]
    public Contracts.Submission GetById(string referenceId)
    {
        return _photoEntryProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<Contracts.Submission> GetAll()
    {
        return _photoEntryProvider.GetAll().ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    [HttpGet("{theme}")]
    public IEnumerable<Contracts.Submission> GetAllByTheme(string theme)
    {
        return _photoEntryProvider.GetAll().Where(o => o.Contest.Theme == theme).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="submission"></param>
    /// <returns></returns>
    [HttpPost]
    public Contracts.Submission CreatePhotoEntry([FromBody] Contracts.Submission submission)
    {
        if (string.IsNullOrWhiteSpace(submission.ReferenceId))
            submission.ReferenceId = Guid.NewGuid().ToString();
        else if (!Guid.TryParse(submission.ReferenceId, out _))
            throw new ValidationException($"Invalid {nameof(submission.ReferenceId)}");

        submission.UploadedOn ??= DateTime.Now;
        return _photoEntryProvider.Insert(submission.ToModel()).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="submission"></param>
    /// <returns></returns>
    [HttpPut("{referenceId}")]
    public Contracts.Submission UpdatePhotoEntry(string referenceId, [FromBody] Contracts.Submission submission)
    {
        if (referenceId != submission.ReferenceId)
            throw new ValidationException($"{nameof(submission.ReferenceId)} does not match within the request");

        _photoEntryProvider.Update(submission.ToModel(), referenceId);
        return _photoEntryProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    [HttpDelete("{referenceId}")]
    public void Delete(string referenceId)
    {
        _photoEntryProvider.Delete(referenceId);
    }
}
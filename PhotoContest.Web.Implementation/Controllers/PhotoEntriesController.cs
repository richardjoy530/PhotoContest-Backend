#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Models;
using PhotoContest.Web.Controllers;
using PhotoContest.Web.Converter;

#endregion

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
///     PhotoEntry Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PhotoEntriesController : ControllerBase, IPhotoEntriesController
{
    private readonly IProvider<PhotoEntry> photoEntryProvider;

    /// <summary>
    ///     Initilises a PhotoEntry Controller
    /// </summary>
    /// <param name="_photoEntryProvider"></param>
    public PhotoEntriesController(IProvider<PhotoEntry> _photoEntryProvider)
    {
        photoEntryProvider = _photoEntryProvider ?? throw new ArgumentNullException(nameof(_photoEntryProvider));
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet("{referenceId}")]
    public Contracts.PhotoEntry GetById(string referenceId)
    {
        return photoEntryProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<Contracts.PhotoEntry> GetAll()
    {
        return photoEntryProvider.GetAll().ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    [HttpGet("{theme}")]
    public IEnumerable<Contracts.PhotoEntry> GetAllByTheme(string theme)
    {
        return photoEntryProvider.GetAll().Where(o => o.Theme.Theme == theme).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="photoEntry"></param>
    /// <returns></returns>
    [HttpPost]
    public Contracts.PhotoEntry CreatePhotoEntry([FromBody] Contracts.PhotoEntry photoEntry)
    {
        if (string.IsNullOrWhiteSpace(photoEntry.ReferenceId))
            photoEntry.ReferenceId = Guid.NewGuid().ToString();
        else if (!Guid.TryParse(photoEntry.ReferenceId, out _))
            throw new ValidationException($"Invalid {nameof(photoEntry.ReferenceId)}");

        if (photoEntry.UploadedOn == null) photoEntry.UploadedOn = DateTime.Now;

        return photoEntryProvider.Insert(photoEntry.ToModel()).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="photoEntry"></param>
    /// <returns></returns>
    [HttpPut("{referenceId}")]
    public Contracts.PhotoEntry UpdatePhotoEntry(string referenceId, [FromBody] Contracts.PhotoEntry photoEntry)
    {
        if (referenceId != photoEntry.ReferenceId)
            throw new ValidationException($"{nameof(photoEntry.ReferenceId)} does not match within the request");

        photoEntryProvider.Update(photoEntry.ToModel(), referenceId);
        return photoEntryProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    [HttpDelete("{referenceId}")]
    public void Delete(string referenceId)
    {
        photoEntryProvider.Delete(referenceId);
    }
}
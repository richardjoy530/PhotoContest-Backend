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
///     PhotographerController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PhotographerController : ControllerBase, IPhotographerController
{
    private readonly IProvider<Photographer> _photographerProvider;

    /// <summary>
    /// </summary>
    /// <param name="photographerProvider"></param>
    public PhotographerController(IProvider<Photographer> photographerProvider)
    {
        _photographerProvider = photographerProvider ?? throw new ArgumentNullException(nameof(photographerProvider));
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    [HttpGet("{referenceId}")]
    public Contracts.Photographer GetById(string referenceId)
    {
        return _photographerProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<Contracts.Photographer> GetAll()
    {
        return _photographerProvider.GetAll().ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="photographer"></param>
    /// <returns></returns>
    [HttpPost]
    public Contracts.Photographer CreatePhotographer([FromBody] Contracts.Photographer photographer)
    {
        if (string.IsNullOrWhiteSpace(photographer.ReferenceId))
            photographer.ReferenceId = Guid.NewGuid().ToString();
        else if (!Guid.TryParse(photographer.ReferenceId, out _))
            throw new ValidationException($"Invalid {nameof(photographer.ReferenceId)}");

        return _photographerProvider.Insert(photographer.ToModel()).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    /// <param name="photographer"></param>
    /// <returns></returns>
    [HttpPut("{referenceId}")]
    public Contracts.Photographer UpdatePhotographer(string referenceId, [FromBody] Contracts.Photographer photographer)
    {
        if (referenceId != photographer.ReferenceId)
            throw new ValidationException($"{nameof(photographer.ReferenceId)} does not match within the request");

        _photographerProvider.Update(photographer.ToModel(), referenceId);
        return _photographerProvider.GetById(referenceId).ToContract();
    }

    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    [HttpDelete("{referenceId}")]
    public void Delete(string referenceId)
    {
        _photographerProvider.Delete(referenceId);
    }
}
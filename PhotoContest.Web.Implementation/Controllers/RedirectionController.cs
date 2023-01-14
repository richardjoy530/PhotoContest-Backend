﻿#region

using Microsoft.AspNetCore.Mvc;

#endregion

namespace WebApi.Controllers;

/// <summary>
/// </summary>
[Route("")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class RedirectionController : ControllerBase
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult RedirectToSwagger()
    {
        return RedirectPermanent("swagger");
    }
}
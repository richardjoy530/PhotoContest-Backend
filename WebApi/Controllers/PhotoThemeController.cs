using Microsoft.AspNetCore.Mvc;
using Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Contracts;

namespace WebApi.Controllers
{
    /// <summary>
    /// PhotoThemeController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoThemeController : ControllerBase
    {
        private readonly IProvider<Provider.Models.Contest> photoThemeProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_photoThemeProvider"></param>
        public PhotoThemeController(IProvider<Provider.Models.Contest> _photoThemeProvider)
        {
            photoThemeProvider = _photoThemeProvider ?? throw new System.ArgumentNullException(nameof(_photoThemeProvider));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        [HttpGet("{referenceId}")]
        public Contest GetById(string referenceId)
        {
            return photoThemeProvider.GetById(referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IEnumerable<Contest> GetAll()
        {
            return photoThemeProvider.GetAll().ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoTheme"></param>
        /// <returns></returns>
        [HttpPost]
        public Contest CreatePhotographer([FromBody] Contest photoTheme)
        {
            if (string.IsNullOrWhiteSpace(photoTheme.ReferenceId))
            {
                photoTheme.ReferenceId = Guid.NewGuid().ToString();
            }
            else if (!Guid.TryParse(photoTheme.ReferenceId, out _))
            {
                throw new ValidationException($"Invalid {nameof(photoTheme.ReferenceId)}");
            }

            return photoThemeProvider.Insert(photoTheme.ToModel()).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="photoTheme"></param>
        /// <returns></returns>
        [HttpPut("{referenceId}")]
        public Contest UpdatePhotographer(string referenceId, [FromBody] Contest photoTheme)
        {
            if (referenceId != photoTheme.ReferenceId)
            {
                throw new ValidationException($"{nameof(photoTheme.ReferenceId)} does not match within the request");
            }

            photoThemeProvider.Update(photoTheme.ToModel(), referenceId);
            return photoThemeProvider.GetById(referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        [HttpDelete("{referenceId}")]
        public void Delete(string referenceId)
        {
            photoThemeProvider.Delete(referenceId);
        }
    }
}

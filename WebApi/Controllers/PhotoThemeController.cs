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
        private readonly IProvider<Provider.Models.PhotoTheme> photoThemeProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_photoThemeProvider"></param>
        public PhotoThemeController(IProvider<Provider.Models.PhotoTheme> _photoThemeProvider)
        {
            photoThemeProvider = _photoThemeProvider ?? throw new System.ArgumentNullException(nameof(_photoThemeProvider));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        [HttpGet("{referenceId}")]
        public PhotoTheme GetById(string referenceId)
        {
            return photoThemeProvider.GetById(referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IEnumerable<PhotoTheme> GetAll()
        {
            return photoThemeProvider.GetAll().ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoTheme"></param>
        /// <returns></returns>
        [HttpPost]
        public PhotoTheme CreatePhotographer([FromBody] PhotoTheme photoTheme)
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
        public PhotoTheme UpdatePhotographer(string referenceId, [FromBody] PhotoTheme photoTheme)
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

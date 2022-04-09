using Microsoft.AspNetCore.Mvc;
using Provider;
using Server.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers
{
    /// <summary>
    /// PhotoEntry Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoEntriesController : ControllerBase
    {
        private readonly IProvider<Provider.Models.PhotoEntry> photoEntryProvider;

        /// <summary>
        /// Initilises a PhotoEntry Controller
        /// </summary>
        /// <param name="_photoEntryProvider"></param>
        public PhotoEntriesController(IProvider<Provider.Models.PhotoEntry> _photoEntryProvider)
        {
            photoEntryProvider = _photoEntryProvider ?? throw new ArgumentNullException(nameof(_photoEntryProvider));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{referenceId}")]
        public PhotoEntry GetById(string referenceId)
        {
            return photoEntryProvider.GetById(referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PhotoEntry> GetAll()
        {
            return photoEntryProvider.GetAll().ToContract();
        }

        /* Implement when data conditional fetching support is added to provider
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        [HttpGet("{theme}")]
        public IEnumerable<PhotoEntry> GetAll(string theme)
        {
            return photoEntryProvider.GetAll().Where(o => o.Theme.Theme == theme).ToContract();
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoEntry"></param>
        /// <returns></returns>
        [HttpPost]
        public PhotoEntry CreatePhotoEntry([FromBody] PhotoEntry photoEntry)
        {
            if (string.IsNullOrWhiteSpace(photoEntry.ReferenceId))
            {
                photoEntry.ReferenceId = Guid.NewGuid().ToString();
            }
            else if (!Guid.TryParse(photoEntry.ReferenceId, out _))
            {
                throw new ValidationException($"Invalid {nameof(photoEntry.ReferenceId)}");
            }

            if (photoEntry.UploadedOn == null)
            {
                photoEntry.UploadedOn = DateTime.Now;
            }

            return photoEntryProvider.Insert(photoEntry.ToModel()).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="photoEntry"></param>
        /// <returns></returns>
        [HttpPut("{referenceId}")]
        public PhotoEntry UpdatePhotoEntry(string referenceId, [FromBody] PhotoEntry photoEntry)
        {
            if (referenceId != photoEntry.ReferenceId)
            {
                throw new ValidationException($"{nameof(photoEntry.ReferenceId)} does not match within the request");
            }
            return photoEntryProvider.Update(photoEntry.ToModel(), referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        [HttpDelete("{referenceId}")]
        public void Delete(string referenceId)
        {
            photoEntryProvider.Delete(referenceId);
        }
    }
}

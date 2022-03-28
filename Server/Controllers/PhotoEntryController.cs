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
    public class PhotoEntryController : ControllerBase
    {
        private readonly IPhotoEntryProvider photoEntryProvider;
        private readonly IReferenceIdMapper referenceIdMapper;

        /// <summary>
        /// Initilises a PhotoEntry Controller
        /// </summary>
        /// <param name="_photoEntryProvider"></param>
        /// <param name="_referenceIdMapper"></param>
        public PhotoEntryController(
            IPhotoEntryProvider _photoEntryProvider,
            IReferenceIdProvider _referenceIdMapper
            )
        {
            photoEntryProvider = _photoEntryProvider;
            referenceIdMapper = _referenceIdMapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PhotoEntry> GetAll()
        {
            return photoEntryProvider.GetPhotoEntries().ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        [HttpGet("{theme}")]
        public IEnumerable<PhotoEntry> GetAll(string theme)
        {
            return photoEntryProvider.GetPhotoEntries(theme).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoEntry"></param>
        /// <returns></returns>
        [HttpPost]
        public PhotoEntry AddPhotoEntry([FromBody] PhotoEntry photoEntry)
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

            return photoEntryProvider.AddPhotoEntry(photoEntry.ToModel()).ToContract();
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
            return photoEntryProvider.UpdatePhotoEntry(photoEntry.ToModel()).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        [HttpDelete("{referenceId}")]
        public void Delete(string referenceId)
        {
            var id = referenceIdMapper.GetIntegerId(referenceId, IdType.PhotoEntry);
            photoEntryProvider.DeletePhotoEntry(id);
        }
    }
}

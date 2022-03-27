using Microsoft.AspNetCore.Mvc;
using Provider;
using Server.Contracts;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoEntryController : ControllerBase
    {
        private readonly IPhotoEntryProvider photoEntryProvider;
        private readonly IReferenceIdMapper referenceIdMapper;

        public PhotoEntryController(
            IPhotoEntryProvider _photoEntryProvider,
            IReferenceIdMapper _referenceIdMapper
            )
        {
            photoEntryProvider = _photoEntryProvider;
            referenceIdMapper = _referenceIdMapper;
        }

        [HttpGet]
        public IEnumerable<PhotoEntry> GetAll()
        {
            return photoEntryProvider.GetPhotoEntries().ToContract();
        }

        [HttpGet("{theme}")]
        public IEnumerable<PhotoEntry> GetAll(string theme)
        {
            return photoEntryProvider.GetPhotoEntries(theme).ToContract();
        }

        [HttpPost]
        public PhotoEntry AddPhotoEntry([FromBody] PhotoEntry photoEntry)
        {
            return photoEntryProvider.AddPhotoEntry(photoEntry.ToModel()).ToContract();
        }

        [HttpPut("{referenceId}")]
        public PhotoEntry UpdatePhotoEntry(string referenceId, [FromBody] PhotoEntry photoEntry)
        {
            if (referenceId != photoEntry.ReferenceId)
            {
                // throw validation
            }
            return photoEntryProvider.UpdatePhotoEntry(photoEntry.ToModel()).ToContract();
        }

        [HttpDelete("{referenceId}")]
        public void Delete(string referenceId)
        {
            var id = referenceIdMapper.GetIntegerId(referenceId, IdType.PhotoEntry);
            photoEntryProvider.DeletePhotoEntry(id);
        }
    }
}

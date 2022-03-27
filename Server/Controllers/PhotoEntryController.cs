using Microsoft.AspNetCore.Mvc;
using Provider;
using Server.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoEntryController : ControllerBase
    {
        private readonly IPhotoEntryProvider photoEntryProvider;
        public PhotoEntryController(IPhotoEntryProvider _photoEntryProvider)
        {
            photoEntryProvider = _photoEntryProvider;
        }
        [HttpGet]
        public IEnumerable<PhotoEntry> Get()
        {
            return photoEntryProvider.GetPhotoEntries();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

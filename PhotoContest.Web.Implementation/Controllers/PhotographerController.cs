using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;
using PhotoContest.Web.Controllers;
using PhotoContest.Web.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Web.Implementation.Controllers
{
    /// <summary>
    /// PhotographerController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotographerController : ControllerBase, IPhotographerController
    {
        private readonly IProvider<Models.Photographer> photographerProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_photographerProvider"></param>
        public PhotographerController(IProvider<Models.Photographer> _photographerProvider)
        {
            photographerProvider = _photographerProvider ?? throw new ArgumentNullException(nameof(_photographerProvider));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        [HttpGet("{referenceId}")]
        public Photographer GetById(string referenceId)
        {
            return photographerProvider.GetById(referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IEnumerable<Photographer> GetAll()
        {
            return photographerProvider.GetAll().ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photographer"></param>
        /// <returns></returns>
        [HttpPost]
        public Photographer CreatePhotographer([FromBody] Photographer photographer)
        {
            if (string.IsNullOrWhiteSpace(photographer.ReferenceId))
            {
                photographer.ReferenceId = Guid.NewGuid().ToString();
            }
            else if (!Guid.TryParse(photographer.ReferenceId, out _))
            {
                throw new ValidationException($"Invalid {nameof(photographer.ReferenceId)}");
            }

            return photographerProvider.Insert(photographer.ToModel()).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="photographer"></param>
        /// <returns></returns>
        [HttpPut("{referenceId}")]
        public Photographer UpdatePhotographer(string referenceId, [FromBody] Photographer photographer)
        {
            if (referenceId != photographer.ReferenceId)
            {
                throw new ValidationException($"{nameof(photographer.ReferenceId)} does not match within the request");
            }

            photographerProvider.Update(photographer.ToModel(), referenceId);
            return photographerProvider.GetById(referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        [HttpDelete("{referenceId}")]
        public void Delete(string referenceId)
        {
            photographerProvider.Delete(referenceId);
        }
    }
}

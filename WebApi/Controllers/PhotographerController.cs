using Microsoft.AspNetCore.Mvc;
using Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Contracts;

namespace WebApi.Controllers
{
    /// <summary>
    /// PhotographerController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotographerController : ControllerBase
    {
        private readonly IProvider<Provider.Models.User> photographerProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_photographerProvider"></param>
        public PhotographerController(IProvider<Provider.Models.User> _photographerProvider)
        {
            photographerProvider = _photographerProvider ?? throw new ArgumentNullException(nameof(_photographerProvider));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        [HttpGet("{referenceId}")]
        public User GetById(string referenceId)
        {
            return photographerProvider.GetById(referenceId).ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IEnumerable<User> GetAll()
        {
            return photographerProvider.GetAll().ToContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photographer"></param>
        /// <returns></returns>
        [HttpPost]
        public User CreatePhotographer([FromBody] User photographer)
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
        public User UpdatePhotographer(string referenceId, [FromBody] User photographer)
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

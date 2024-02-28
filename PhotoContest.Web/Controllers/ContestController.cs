using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Services;
using PhotoContest.Web.Contracts;
using PhotoContest.Web.Converters;

namespace PhotoContest.Web.Controllers
{
    /// <summary>
    /// ContestController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContestController : ControllerBase
    {
        private readonly IContestManagementService _contestManagementService;
    
        /// <summary>
        /// Initialises ContestController
        /// </summary>
        public ContestController(IContestManagementService contestManagementService)
        {
            _contestManagementService = contestManagementService ?? throw new ArgumentNullException(nameof(contestManagementService));
        }

        /// <summary>
        /// </summary>
        /// <param name="contest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> Create([FromBody] ContestRequest contest)
        {
            return Ok(_contestManagementService.Create(contest.Theme, contest.EndDate));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult<ContestResponse[]> GetAll()
        {
            return Ok(_contestManagementService.GetAll().Select(ConverterExtensions.ToResponse));
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPut("{id:int}/end-date")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ContestResponse> UpdateEndDate(int id, [FromBody] DateTime endDate)
        {
            _contestManagementService.UpdateEndDate(id, endDate);
            _contestManagementService.TryGet(id, out var contest);
            return Ok(contest.ToResponse());
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="theme"></param>
        /// <returns></returns>
        [HttpPut("{id:int}/update-theme")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ContestResponse> UpdateTheme(int id, [FromBody] string theme)
        {
            _contestManagementService.UpdateTheme(id, theme);
            _contestManagementService.TryGet(id, out var contest);
            return Ok(contest.ToResponse());
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ContestResponse> Get(int id)
        {
            _contestManagementService.TryGet(id, out var contest);
            return Ok(contest.ToResponse());
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ContestResponse> GetCurrent()
        {
            return Ok(_contestManagementService.CurrentContest.ToResponse());
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            return Ok(_contestManagementService.Delete(id));
        }
    }
}
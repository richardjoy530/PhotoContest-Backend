using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;
using PhotoContest.Web.Controllers;

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ContestController : ControllerBase, IContestController
{
    private readonly IContestService _contestService;

    /// <summary>
    /// </summary>
    public ContestController(
        IContestService contestService
    )
    {
        _contestService = contestService ?? throw new ArgumentNullException(nameof(contestService));
    }

    /// <inheritdoc />
    [HttpPost]
    public int CreateContest(Contest contest)
    {
        return _contestService.AddContest(contest.Theme, contest.EndDate);
    }

    /// <inheritdoc />
    [HttpGet]
    public Contest[] GetAll()
    {
        return _contestService.GetAllContests().Select(ToContract).ToArray();
    }

    private static Contest ToContract(Models.Contest arg)
    {
        return new Contest
        {
            EndDate = arg.EndDate,
            Theme = arg.Theme,
            Id = arg.Id
        };
    }
}
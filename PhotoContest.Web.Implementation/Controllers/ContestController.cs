using System;
using PhotoContest.Web.Contracts;
using PhotoContest.Web.Controllers;

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
/// 
/// </summary>
public class ContestController : IContestController
{
    private readonly IContestService _contestService;
    /// <summary>
    /// 
    /// </summary>
    public ContestController(
        IContestService contestService
        )
    {
        _contestService = contestService ?? throw new ArgumentNullException(nameof(contestService));
    }
    
    /// <inheritdoc />
    public int CreateContest(Contest contest)
    {
        return _contestService.AddContest(contest.Theme, contest.EndDate);
    }
    
    /// <inheritdoc />
    public Contest GetCurrentContest()
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc />
    public Contest GetContestById(int id)
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc />
    public Contest[] GetAllContests()
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc />
    public void AddPhotoSubmission(Submission submission)
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc />
    public Submission[] GetCurrentSubmissions()
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc />
    public void Vote(int submissionId, int position)
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc />
    public VoteInfo GetVoteInfo()
    {
        throw new NotImplementedException();
    }
}
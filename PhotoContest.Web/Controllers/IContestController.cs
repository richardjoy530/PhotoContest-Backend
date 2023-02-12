#region

using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Controllers;

/// <summary>
/// </summary>
public interface IContestController
{
    /// <summary>
    /// </summary>
    /// <param name="contest"></param>
    /// <returns></returns>
    int CreateContest([FromBody] Contest contest);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Contest GetCurrentContest();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Contest GetContestById(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Contest[] GetAllContests();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="submission"></param>
    void AddPhotoSubmission(Submission submission);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Submission[] GetCurrentSubmissions();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="submissionId"></param>
    /// <param name="position"></param>
    void Vote(int submissionId, int position);

    /// <summary>
    /// Current user's vote details for the current contest
    /// </summary>
    VoteInfo GetVoteInfo();
}
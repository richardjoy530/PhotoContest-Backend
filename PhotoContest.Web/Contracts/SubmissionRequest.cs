#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace PhotoContest.Web.Contracts;

/// <summary>
/// Contains the details of Submission
/// </summary>
public class SubmissionRequest
{
    /// <summary>
    /// Integer id of the contest
    /// </summary>
    [Required]
    public int ContestId { get; set; }

    /// <summary>
    /// Integer id of the  associated photo.
    /// </summary>
    [Required]
    public int FileId { get; set; }

    /// <summary>
    /// A creative caption to go in with your photo
    /// </summary>
    [Required]
    public string Caption { get; set; }

    //todo get user from token
    /// <summary>
    /// Details of the photographer who uploaded the photo
    /// </summary>
    public int UserId { get; set; }
}
using System;

namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public record Submission : IRecord
{
    /// <summary>
    /// 
    /// </summary>
    public int Id;

    /// <summary>
    /// 
    /// </summary>
    public int ContestId;

    /// <summary>
    /// 
    /// </summary>
    public int FileInfoId;
    
    /// <summary>
    /// 
    /// </summary>
    public string Caption;
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime UploadedOn;
    
    /// <summary>
    /// 
    /// </summary>
    public int UserId;
}
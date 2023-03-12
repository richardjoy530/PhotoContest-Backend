using System;

namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public record Submission : IDataRecord
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
    
    /// <summary>
    /// 
    /// </summary>
    public string RefId;
}
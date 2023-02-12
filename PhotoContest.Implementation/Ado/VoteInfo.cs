namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public record VoteInfo : IDataRecord
{
    /// <summary>
    /// 
    /// </summary>
    public int Id;

    /// <summary>
    /// 
    /// </summary>
    public int FirstId;
    
    /// <summary>
    /// 
    /// </summary>
    public int SecondId;
    
    /// <summary>
    /// 
    /// </summary>
    public int ThirdId;
    
    /// <summary>
    /// 
    /// </summary>
    public int ContestId;
    
    /// <summary>
    /// 
    /// </summary>
    public int UserId;
}
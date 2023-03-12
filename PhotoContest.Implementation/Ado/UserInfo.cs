using System;

namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public record UserInfo : IDataRecord
{
    /// <summary>
    /// 
    /// </summary>
    public int Id;
    
    /// <summary>
    /// 
    /// </summary>
    public string Name;

    /// <summary>
    /// 
    /// </summary>
    public string Email;

    /// <summary>
    /// 
    /// </summary>
    public string RefId;

    /// <summary>
    /// 
    /// </summary>
    public DateTime RegistrationDate;
}
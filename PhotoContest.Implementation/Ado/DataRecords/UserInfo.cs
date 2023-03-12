using System;

namespace PhotoContest.Implementation.Ado.DataRecords;

/// <summary>
/// </summary>
public record UserInfo : IDataRecord
{
    /// <summary>
    /// </summary>
    public string Email;

    /// <summary>
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// </summary>
    public string Name;

    /// <summary>
    /// </summary>
    public string RefId;

    /// <summary>
    /// </summary>
    public DateTime RegistrationDate;
}
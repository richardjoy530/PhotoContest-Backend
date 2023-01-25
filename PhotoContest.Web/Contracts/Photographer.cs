namespace PhotoContest.Web.Contracts;

/// <summary>
///     Contains details of Photographer
/// </summary>
public class Photographer
{
    /// <summary>
    ///     Reference Id of the photographer
    /// </summary>
    public string ReferenceId { get; set; }

    /// <summary>
    ///     Name of the photographer
    /// </summary>
    public string UploaderName { get; set; }
}
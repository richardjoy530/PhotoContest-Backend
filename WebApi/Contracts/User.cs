namespace WebApi.Contracts
{
    /// <summary>
    /// Contains details of User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Reference Id of the phographer
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Name of the photographer
        /// </summary>
        public string UploaderName { get; set; }
    }
}

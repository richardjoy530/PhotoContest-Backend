namespace PhotoContest.Models
{
    /// <summary>
    ///     FileInfo
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        ///     Id
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        ///     Path of the location where the file is saved
        /// </summary>
        public string Path { get; set; }
    
        /// <summary>
        /// RefId
        /// </summary>
        public string RefId  { get; set; }
    }
}
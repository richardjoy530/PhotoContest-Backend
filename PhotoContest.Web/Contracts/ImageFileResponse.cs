#region

using System.Text.Json.Serialization;

#endregion

namespace PhotoContest.Web.Contracts
{
    //todo: firebase | azure | system file
    /// <summary>
    /// Response class containing uploaded image
    /// </summary>
    public class ImageFileResponse : ImageFileRequest
    {
        /// <summary>
        /// Unique integer id representing an image entry
        /// </summary>
        public int Id;

        /// <summary>
        /// Path of the image
        /// </summary>
        [JsonIgnore]
        public string Path;
    }
}
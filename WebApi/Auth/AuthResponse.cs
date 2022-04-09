namespace WebApi.Auth
{
    /// <summary>
    /// Encapsulates response during auth
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Status indicating if the request was succeusfull or errored
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Message describing the response
        /// </summary>
        public string Message { get; set; }
    }
}

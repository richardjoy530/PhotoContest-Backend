using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Web.Contracts
{
    /// <summary>
    /// Contains details of UserInfoRequest
    /// </summary>
    public class UserInfoRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name;

        /// <summary>
        /// Email Id
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email;
    }
}
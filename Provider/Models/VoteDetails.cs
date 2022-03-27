namespace Provider.Models
{
    public class VoteDetails
    {
        /// <summary>
        /// Reference Id of the PhotoEntry
        /// </summary>
        public string EntryId { get; set; }

        /// <summary>
        /// Score of the referenced PhotoEnty
        /// </summary>
        public int Score { get; set; }
    }
}

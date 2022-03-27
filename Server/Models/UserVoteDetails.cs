namespace Server.Models
{
    public class UserVoteDetails
    {
        public Photographer Photographer { get; set; }

        public string FirstVoteID { get; set; }

        public string SecondVoteID { get; set; }

        public string ThirdVoteID { get; set; }     
    }
}

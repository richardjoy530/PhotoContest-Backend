using System;

namespace PhotoContest.Web.Contracts
{
   /// <summary>
   /// Response class containing details of a submission
   /// </summary>
   public class SubmissionResponse : SubmissionRequest
   {
      /// <summary>
      /// Unique integer id for this submission
      /// </summary>
      public int Id;

      /// <summary>
      /// Time when this submission was made
      /// </summary>
      public DateTime UploadedOn;
   }
}
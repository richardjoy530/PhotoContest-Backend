using System;

namespace Provider.Models
{
    public class PhotoEntry
    {
        public string ReferenceID { get; set; }

        public string Theme { get; set; }

        public Photographer Photographer { get; set; }

        public string FileId { get; set; }

        public string Caption { get; set; }

        public DateTime UploadedOn { get; set; }
    }
}

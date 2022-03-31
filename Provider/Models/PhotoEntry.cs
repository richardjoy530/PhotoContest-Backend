using System;

namespace Provider.Models
{
    /// <summary>
    /// Contains the details of PhotoEntry
    /// </summary>
    public class PhotoEntry : IDbModel
    {
        /// <summary>
        /// Id details of the PhotoEntry record
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Theme of the photo. This must correspond to <see cref="PhotoTheme"/>
        /// </summary>
        public PhotoTheme Theme { get; set; }

        /// <summary>
        /// Details of the photographer who uploaded the photo
        /// </summary>
        public Photographer Photographer { get; set; }

        /// <summary>
        /// Id details of the associated photo. This should always be valid.
        /// </summary>
        public Id FileId { get; set; }

        /// <summary>
        /// A creative caption to go in with your photo
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Date and time of the upload
        /// </summary>
        public DateTime UploadedOn { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType = IdType.PhotoEntry)
        {
            Id.ResolveIntegerId(mapper, idType);
            Theme.ResolveIntegerId(mapper);
            FileId.ResolveIntegerId(mapper, IdType.File);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.PhotoEntry)
        {
            Id.ResolveReferenceId(mapper, idType);
            Theme.ResolveReferenceId(mapper);
            FileId.ResolveReferenceId(mapper, IdType.File);
            IsResolved = true;
        }
    }
}

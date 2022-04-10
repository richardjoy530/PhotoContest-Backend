using System;
using System.Data;

namespace Provider.Models
{
    /// <summary>
    /// Contains the details of PhotoEntry
    /// </summary>
    public class PhotoEntry : IDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        public PhotoEntry(string referenceId)
        {
            Id = new Id { ReferenceId = referenceId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integerId"></param>
        public PhotoEntry(int integerId)
        {
            Id = new Id { IntegerId = integerId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord"></param>
        public PhotoEntry(IDataRecord dataRecord)
        {
            Id = new Id { IntegerId = (int)dataRecord["Id"] };
            Theme = new PhotoTheme((int)dataRecord["Id"]);
            FileId = new Id { IntegerId = (int)dataRecord["FileId"] };
            Caption = (string)dataRecord["Caption"];
            // TODO: Change this to go fetch the photographer from cache.
            Photographer = new Photographer((int)dataRecord["PhotographerId"]);
            UploadedOn = (DateTime)dataRecord["UploadedOn"];
        }

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
        public void ResolveIntegerId(IReferenceIdMapper mapper)
        {
            Id.ResolveIntegerId(mapper);
            Theme.ResolveIntegerId(mapper);
            FileId.ResolveIntegerId(mapper);
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

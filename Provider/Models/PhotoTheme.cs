using System;

namespace Provider.Models
{
    /// <summary>
    /// Contains details of PhotoTheme
    /// </summary>
    public class PhotoTheme : IDbModel
    {
        /// <summary>
        /// Id details of the <see cref="PhotoTheme"/> record
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Theme of the photo contest
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Date of the contest
        /// </summary>
        public DateTime ContestDate { get; set; }

        /// <inheritdoc/>
        public bool IsResolved { get; set; }

        /// <inheritdoc/>
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType = IdType.Theme)
        {
            Id.IntegerId = mapper.GetIntegerId(Id.ReferenceId, idType);
            IsResolved = true;
        }

        /// <inheritdoc/>
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.Theme)
        {
            Id.ReferenceId = mapper.GetReferenceId(Id.IntegerId, idType);
            IsResolved = true;
        }
    }
}

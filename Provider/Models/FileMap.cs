using System;

namespace Provider.Models
{
    /// <summary>
    /// FileMap
    /// </summary>
    public class FileMap : IDbModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Path of the location where the file is saved
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsResolved { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType = IdType.File)
        {
            Id.IntegerId = mapper.GetIntegerId(Id.ReferenceId, idType);
            IsResolved = true;
        }

        /// <inheritdoc/>
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.File)
        {
            Id.ReferenceId = mapper.GetReferenceId(Id.IntegerId, idType);
            IsResolved = true;
        }
    }
}

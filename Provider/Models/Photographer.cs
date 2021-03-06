using System.Data;

namespace Provider.Models
{
    /// <summary>
    /// Contains details of Photographer
    /// </summary>
    public class Photographer : IDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        public Photographer(string referenceId)
        {
            Id = new Id { ReferenceId = referenceId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integerId"></param>
        public Photographer(int integerId)
        {
            Id = new Id { IntegerId = integerId };
        }

        /// <summary>
        /// 
        /// </summary>
        public Photographer(IDataRecord dataRecord)
        {
            Id = new Id { IntegerId = (int)dataRecord["Id"] };
            UploaderName = (string)dataRecord["UploaderName"];
        }

        /// <summary>
        /// Id details of the <see cref="Photographer"/> record
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Name of the photographer
        /// </summary>
        public string UploaderName { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper)
        {
            Id.ResolveIntegerId(mapper);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.Photographer)
        {
            Id.ResolveReferenceId(mapper, idType);
            IsResolved = true;
        }
    }
}

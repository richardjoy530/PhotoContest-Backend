using System.Data;

namespace Provider.Models
{
    /// <summary>
    /// Contains details of User
    /// </summary>
    public class User : IDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        public User(string referenceId)
        {
            Id = new Id { ReferenceId = referenceId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integerId"></param>
        public User(int integerId)
        {
            Id = new Id { IntegerId = integerId };
        }

        /// <summary>
        /// 
        /// </summary>
        public User(IDataRecord dataRecord)
        {
            Id = new Id { IntegerId = (int)dataRecord["Id"] };
            UploaderName = (string)dataRecord["UploaderName"];
        }

        /// <summary>
        /// Id details of the <see cref="User"/> record
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
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.User)
        {
            Id.ResolveReferenceId(mapper, idType);
            IsResolved = true;
        }
    }
}

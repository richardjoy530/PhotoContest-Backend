using System;
using System.Data;

namespace PhotoContest.Models
{
    /// <summary>
    /// Contains details of PhotoTheme
    /// </summary>
    public class PhotoTheme : IDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        public PhotoTheme(string referenceId)
        {
            Id = new Id { ReferenceId = referenceId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integerId"></param>
        public PhotoTheme(int integerId)
        {
            Id = new Id { IntegerId = integerId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord"></param>
        public PhotoTheme(IDataRecord dataRecord)
        {
            Id = new Id { IntegerId = (int)dataRecord["Id"] };
            Theme = (string)dataRecord["Theme"];
            ContestDate = (DateTime)dataRecord["ContestDate"];
        }

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
        public void ResolveIntegerId(IReferenceIdMapper mapper)
        {
            Id.IntegerId = mapper.GetIntegerId(Id.ReferenceId);
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

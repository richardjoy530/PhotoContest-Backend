using Provider.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Provider.Implementation
{
    /// <summary>
    /// Database access layer of <see cref="Photographer"/>
    /// </summary>
    public class PhotographerProvider : IPhotographerProvider
    {
        private readonly string connectionString;
        private readonly IReferenceIdProvider referenceIdProvider;

        /// <summary>
        /// Initializes a new instance of PhotographerProvider class
        /// </summary>
        /// <param name="_dbConnection"></param>
        /// <param name="_referenceIdProvider"></param>
        public PhotographerProvider(
            IDbConnection _dbConnection,
            IReferenceIdProvider _referenceIdProvider
            )
        {
            if (_dbConnection is null)
            {
                throw new ArgumentNullException(nameof(_dbConnection));
            }

            connectionString = _dbConnection.ConnectionString;
            referenceIdProvider = _referenceIdProvider ?? throw new ArgumentNullException(nameof(_referenceIdProvider));
        }

        /// <inheritdoc/>
        public Photographer AddPhotographer(Photographer photographer)
        {
            if (!Guid.TryParse(photographer.Id.ReferenceId, out _))
            {
                photographer.Id.ReferenceId = Guid.NewGuid().ToString();
            }

            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("INSERT INTO [dbo].[Photographer] (");
                qs.Append("[UploaderName] ");
                qs.Append(") ");
                qs.Append("OUTPUT [INSERTED].[Id] ");
                qs.Append("VALUES ( ");
                qs.Append("@UploaderName )");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                command.Parameters.AddWithValue("@UploaderName", photographer.UploaderName);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                photographer.Id.IntegerId = reader.GetInt32(0);
            }
            referenceIdProvider.InsertIdMap(photographer.Id, IdType.Photographer);
            return photographer;
        }

        /// <inheritdoc/>
        public void DeletePhotographer(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Photographer GetPhotographer(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Photographer> GetPhotographers()
        {
            throw new NotImplementedException();
        }
    }
}

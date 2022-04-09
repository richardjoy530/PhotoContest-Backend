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
    public class PhotographerProvider : IProvider<Photographer>
    {
        private readonly string connectionString;
        private readonly IReferenceIdMapper referenceIdMapper;

        /// <summary>
        /// Initializes a new instance of PhotographerProvider class
        /// </summary>
        /// <param name="_dbConnection"></param>
        /// <param name="_referenceIdMapper"></param>
        public PhotographerProvider(
            IDbConnection _dbConnection,
            IReferenceIdMapper _referenceIdMapper
            )
        {
            if (_dbConnection is null)
            {
                throw new ArgumentNullException(nameof(_dbConnection));
            }

            connectionString = _dbConnection.ConnectionString;
            referenceIdMapper = _referenceIdMapper ?? throw new ArgumentNullException(nameof(_referenceIdMapper));
        }

        /// <inheritdoc/>
        public Photographer Insert(Photographer photographer)
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
            referenceIdMapper.InsertIdMap(photographer.Id, IdType.Photographer);
            return photographer;
        }

        /// <inheritdoc/>
        public void Delete(string referenceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Photographer GetById(string referenceId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Photographer> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Photographer Update(Photographer photographer, string referenceId)
        {
            throw new NotImplementedException();
        }
    }
}

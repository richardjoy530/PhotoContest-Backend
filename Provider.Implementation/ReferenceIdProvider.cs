using Provider.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Provider.Implementation
{
    /// <summary>
    /// Mapps reference id to integer id from database.
    /// </summary>
    public class ReferenceIdProvider : IReferenceIdProvider
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of ReferenceIdProvider class
        /// </summary>
        /// <param name="_dbConnection"></param>
        public ReferenceIdProvider(IDbConnection _dbConnection)
        {
            if (_dbConnection is null)
            {
                throw new ArgumentNullException(nameof(_dbConnection));
            }

            connectionString = _dbConnection.ConnectionString;
        }

        ///<inheritdoc/>
        public int GetIntegerId(string referenceId, IdType idType)
        {
            int id;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("SELECT ");
                qs.Append("[Id], ");
                qs.Append("FROM [dbo].[IdMap] ");
                qs.Append("WHERE ");
                qs.Append("[ReferenceId]=@ReferenceId ");
                qs.Append("AND ");
                qs.Append("[IdType]=@IdType ");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                command.Parameters.AddWithValue("@ReferenceId", new Guid(referenceId));
                command.Parameters.AddWithValue("@IdType", (int)idType);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
            }
            return id;
        }

        ///<inheritdoc/>
        public string GetReferenceId(int id, IdType idType)
        {
            string referenceId;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("SELECT ");
                qs.Append("[ReferenceId] ");
                qs.Append("FROM [dbo].[IdMap] ");
                qs.Append("WHERE ");
                qs.Append("[Id]=@Id ");
                qs.Append("AND ");
                qs.Append("[IdType]=@IdType ");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@IdType", (int)idType);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                referenceId = reader.GetGuid(0).ToString();
            }
            return referenceId;
        }

        ///<inheritdoc/>
        public void InsertIdMap(Id id, IdType idType)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            var qs = new StringBuilder();
            qs.Append("INSERT INTO [dbo].[IdMap] (");
            qs.Append("[Id], ");
            qs.Append("[ReferenceId], ");
            qs.Append("[IdType] ");
            qs.Append(") VALUES ( ");
            qs.Append("@Id, ");
            qs.Append("@ReferenceId, ");
            qs.Append("@IdType )");
            var sql = qs.ToString();
            using SqlCommand command = new(sql, conncetion);
            command.Parameters.AddWithValue("@Id", id.IntegerId);
            command.Parameters.AddWithValue("@ReferenceId", new Guid(id.ReferenceId));
            command.Parameters.AddWithValue("@IdType", (int)idType);
            command.ExecuteNonQuery();
        }
    }
}

using Provider.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Provider.Implementation
{
    /// <summary>
    /// Mapps reference id to integer id from database.
    /// </summary>
    public class ReferenceIdProvider : IReferenceIdMapper
    {
        private readonly string connectionString;
        private readonly string InsertProcedure = "[dbo].[Insert_IdMap]";
        private readonly string GetIdProcedure = "[dbo].[GetId_IdMap]";
        private readonly string GetRefProcedure = "[dbo].[GetRef_IdMap]";
        private readonly string GetProcedure = "[dbo].[Get_IdMap]";
        private readonly string DeleteProcedure = "[dbo].[Delete_IdMap]";

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
        public int GetIntegerId(string referenceId)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetIdProcedure;
            command.Parameters.Add(new SqlParameter("@ReferenceId", referenceId));
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            return Convert.ToInt32(command.Parameters["@Id"].Value);
        }

        ///<inheritdoc/>
        public string GetReferenceId(int id, IdType idType)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetRefProcedure;
            command.Parameters.Add(new SqlParameter("@Id", id));
            command.Parameters.Add(new SqlParameter("@IdType", (int)idType));
            command.Parameters.Add("@ReferenceId", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            return command.Parameters["@ReferenceId"].Value.ToString();
        }

        ///<inheritdoc/>
        public void InsertIdMap(Id id, IdType idType)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = InsertProcedure;
            command.Parameters.Add(new SqlParameter("@Id", id.IntegerId));
            command.Parameters.Add(new SqlParameter("@ReferenceId", id.ReferenceId));
            command.Parameters.Add(new SqlParameter("@IdType", (int)idType));
            command.ExecuteNonQuery();
        }
    }
}

using Provider.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Provider.Implementation
{
    /// <summary>
    /// Database access layer of <see cref="Contest"/>
    /// </summary>
    public class ContestProvider : IProvider<Contest>
    {
        private readonly string connectionString;
        private readonly IReferenceIdMapper referenceIdMapper;
        private readonly string InsertProcedure = "[dbo].[Insert_Photographer]";
        private readonly string GetByIdProcedure = "[dbo].[GetById_Photographer]";
        private readonly string GetProcedure = "[dbo].[Get_Photographer]";
        private readonly string UpdateProcedure = "[dbo].[Update_Photographer]";
        private readonly string DeleteProcedure = "[dbo].[Delete_Photographer]";

        /// <summary>
        /// Initializes a new instance of ContestProvider class
        /// </summary>
        /// <param name="_dbConnection"></param>
        /// <param name="_referenceIdMapper"></param>
        public ContestProvider(
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
        public Contest Insert(Contest photographer)
        {
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = InsertProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@Contest", photographer.Theme));
                command.Parameters.Add(new SqlParameter("@EndDate", photographer.EndDate));
                command.ExecuteNonQuery();
                photographer.Id.IntegerId = Convert.ToInt32(command.Parameters["@Id"].Value);
            }
            return photographer;
        }

        /// <inheritdoc/>
        public void Delete(string referenceId)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = DeleteProcedure;
            command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
            command.ExecuteNonQuery();
        }

        /// <inheritdoc/>
        public Contest GetById(string referenceId)
        {
            Contest photographer;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetByIdProcedure;
                command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                photographer = new Contest(reader);
            }
            return photographer;
        }

        /// <inheritdoc/>
        public IEnumerable<Contest> GetAll()
        {
            var photographers = new List<Contest>();
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetProcedure;
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var photographer = new Contest(reader);
                    photographer.ResolveReferenceId(referenceIdMapper);
                    photographers.Add(photographer);
                }
            }
            return photographers;
        }

        /// <inheritdoc/>
        public void Update(Contest photographer, string referenceId)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = UpdateProcedure;
            command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
            command.Parameters.Add(new SqlParameter("@Contest", photographer.Theme));
            command.Parameters.Add(new SqlParameter("@EndDate", photographer.EndDate));
            command.ExecuteNonQuery();
        }
    }
}

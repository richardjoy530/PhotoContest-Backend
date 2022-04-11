using Provider.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Provider.Implementation
{
    /// <summary>
    /// Database access layer of <see cref="PhotoTheme"/>
    /// </summary>
    public class PhotoThemeProvider : IProvider<PhotoTheme>
    {
        private readonly string connectionString;
        private readonly IReferenceIdMapper referenceIdMapper;
        private readonly string InsertProcedure = "[dbo].[Insert_Photographer]";
        private readonly string GetByIdProcedure = "[dbo].[GetById_Photographer]";
        private readonly string GetProcedure = "[dbo].[Get_Photographer]";
        private readonly string UpdateProcedure = "[dbo].[Update_Photographer]";
        private readonly string DeleteProcedure = "[dbo].[Delete_Photographer]";

        /// <summary>
        /// Initializes a new instance of PhotoThemeProvider class
        /// </summary>
        /// <param name="_dbConnection"></param>
        /// <param name="_referenceIdMapper"></param>
        public PhotoThemeProvider(
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
        public PhotoTheme Insert(PhotoTheme photographer)
        {
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = InsertProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@Theme", photographer.Theme));
                command.Parameters.Add(new SqlParameter("@ContestDate", photographer.ContestDate));
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
        public PhotoTheme GetById(string referenceId)
        {
            PhotoTheme photographer;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetByIdProcedure;
                command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                photographer = new PhotoTheme(reader);
            }
            return photographer;
        }

        /// <inheritdoc/>
        public IEnumerable<PhotoTheme> GetAll()
        {
            var photographers = new List<PhotoTheme>();
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetProcedure;
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var photographer = new PhotoTheme(reader);
                    photographer.ResolveReferenceId(referenceIdMapper);
                    photographers.Add(photographer);
                }
            }
            return photographers;
        }

        /// <inheritdoc/>
        public void Update(PhotoTheme photographer, string referenceId)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = UpdateProcedure;
            command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
            command.Parameters.Add(new SqlParameter("@Theme", photographer.Theme));
            command.Parameters.Add(new SqlParameter("@ContestDate", photographer.ContestDate));
            command.ExecuteNonQuery();
        }
    }
}

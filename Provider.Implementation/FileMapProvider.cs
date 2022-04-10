using Provider.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Provider.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class FileMapProvider : IProvider<FileMap>
    {
        private readonly string connectionString;
        private readonly IReferenceIdMapper referenceIdMapper;
        private readonly string InsertProcedure = "[dbo].[Insert_FileMap]";
        private readonly string GetByIdProcedure = "[dbo].[GetById_FileMap]";
        private readonly string GetProcedure = "[dbo].[Get_FileMap]";
        private readonly string UpdateProcedure = "[dbo].[Update_FileMap]";
        private readonly string DeleteProcedure = "[dbo].[Delete_FileMap]";

        /// <summary>
        /// 
        /// </summary>
        public FileMapProvider(
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
        public FileMap Insert(FileMap fileMap)
        {
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = InsertProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@FilePath", fileMap.FilePath));
                command.ExecuteNonQuery();
                fileMap.Id.IntegerId = Convert.ToInt32(command.Parameters["@Id"].Value);
            }
            return fileMap;
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
        public IEnumerable<FileMap> GetAll()
        {
            var fileMaps = new List<FileMap>();
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetProcedure;
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var fileMap = new FileMap(reader);
                    fileMap.ResolveReferenceId(referenceIdMapper);
                    fileMaps.Add(fileMap);
                }
            }
            return fileMaps;
        }

        /// <inheritdoc/>
        public FileMap GetById(string referenceId)
        {
            FileMap fileMap;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetProcedure;
                command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                fileMap = new FileMap(reader);
            }
            return fileMap;
        }

        /// <inheritdoc/>
        public void Update(FileMap fileMap, string referenceId)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = UpdateProcedure;
            command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
            command.Parameters.Add(new SqlParameter("@FilePath", fileMap.FilePath));
            command.ExecuteNonQuery();
        }
    }
}

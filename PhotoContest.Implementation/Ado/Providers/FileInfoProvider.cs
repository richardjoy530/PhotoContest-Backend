using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Ado.Providers
{
    /// <summary>
    ///     Database access layer of <see cref="Contest" />
    /// </summary>
    public class FileInfoProvider : IProvider<FileInfo>
    {
        private const string GetByIdProcedure = "[dbo].[FileInfo_GetById]";
        private const string GetProcedure = "[dbo].[FileInfo_GetAll]";
        private const string InsertProcedure = "[dbo].[FileInfo_Insert]";
        private const string UpdateProcedure = "[dbo].[FileInfo_Update]";
        private const string DeleteProcedure = "[dbo].[FileInfo_Delete]";
        private const string GetAllIdsProcedure = "[dbo].[FileInfo_GetAllIds]";
        private readonly string _connectionString;

        /// <summary>
        ///     Initializes a new instance of ContestProvider class
        /// </summary>
        /// <param name="dbConnection"></param>
        public FileInfoProvider(IDatabase dbConnection)
        {
            if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

            _connectionString = dbConnection.ConnectionString;
        }

        /// <inheritdoc />
        public int Insert(FileInfo data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            if (data.Id != 0) throw new ArgumentException("Id must be 0 while inserting");

            if (string.IsNullOrWhiteSpace(data.Path))
                throw new ArgumentException($"{nameof(data.Path)} is null or empty");

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = InsertProcedure;
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add(new SqlParameter("@Path", data.Path));
            command.ExecuteNonQuery();

            return data.Id = Convert.ToInt32(command.Parameters["@Id"].Value);
        }

        /// <inheritdoc />
        public bool Delete(int id)
        {
            if (id < 1) throw new ArgumentException("Database id must not be less than 1");

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = DeleteProcedure;
            command.Parameters.Add(new SqlParameter("@Id", id));
            return command.ExecuteNonQuery() > 0;
        }

        /// <inheritdoc />
        public FileInfo GetById(int id)
        {
            if (id < 1) throw new ArgumentException("Database id must not be less than 1");

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetByIdProcedure;
            command.Parameters.Add(new SqlParameter("@Id", id));
            using var reader = command.ExecuteReader();
            reader.Read();
            return ParseData(reader);
        }

        /// <inheritdoc />
        public int[] GetAllIds()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetAllIdsProcedure;
            using var reader = command.ExecuteReader();
            var ids = new Collection<int>();
            while (reader.Read())
                ids.Add((int)reader["Id"]);
            return ids.ToArray();
        }

        /// <inheritdoc />
        public IEnumerable<FileInfo> GetAll()
        {
            var contests = new List<FileInfo>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetProcedure;
            using var reader = command.ExecuteReader();
            while (reader.Read())
                contests.Add(ParseData(reader));

            return contests;
        }

        /// <inheritdoc />
        public bool Update(FileInfo data, long updateParamsLong = (long)FileInfoParams.None)
        {
            var updateParams = (FileInfoParams)updateParamsLong;
            if (data.Id < 1) throw new ArgumentException("Database Id must not be less than 1");

            if ((FileInfoParams.Path & updateParams) == FileInfoParams.Path && string.IsNullOrWhiteSpace(data.Path))
                throw new ArgumentException($"{nameof(data.Path)} is null or empty");

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = UpdateProcedure;
            command.Parameters.Add(new SqlParameter("@Id", data.Id));
            command.Parameters.Add(new SqlParameter("@Path", data.Path));
            command.Parameters.Add(new SqlParameter("@UpdatePath", FileInfoParams.Path & updateParams));
            return command.ExecuteNonQuery() > 0;
        }

        private static FileInfo ParseData(System.Data.IDataRecord record)
        {
            if (record is null)
                throw new ArgumentNullException(nameof(record));

            return new FileInfo
            {
                Path = (string)record["Path"],
                Id = (int)record["Id"]
            };
        }
    }
}
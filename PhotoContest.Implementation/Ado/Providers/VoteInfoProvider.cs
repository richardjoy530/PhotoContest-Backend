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
    ///     Database access layer of <see cref="VoteInfo" />
    /// </summary>
    public class VoteInfoProvider : IProvider<VoteInfo>
    {
        private const string GetByIdProcedure = "[dbo].[VoteInfo_GetById]";
        private const string GetProcedure = "[dbo].[VoteInfo_GetAll]";
        private const string GetAllIdsProcedure = "[dbo].[VoteInfo_GetAllIds]";
        private const string InsertProcedure = "[dbo].[VoteInfo_Insert]";
        private const string UpdateProcedure = "[dbo].[VoteInfo_Update]";
        private const string DeleteProcedure = "[dbo].[VoteInfo_Delete]";
        private readonly string _connectionString;

        /// <summary>
        ///     Initializes a new instance of PhotoEntryProvider class
        /// </summary>
        /// <param name="dbConnection"></param>
        public VoteInfoProvider(IDatabase dbConnection)
        {
            if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

            _connectionString = dbConnection.ConnectionString;
        }

        /// <inheritdoc />
        public VoteInfo GetById(int id)
        {
            if (id < 1)
                throw new ArgumentException("Database Id must not be less than 1");

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
        public IEnumerable<VoteInfo> GetAll()
        {
            var data = new List<VoteInfo>();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetProcedure;
            using var reader = command.ExecuteReader();
            while (reader.Read())
                data.Add(ParseData(reader));

            return data;
        }

        /// <inheritdoc />
        public int Insert(VoteInfo data)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            if (data.Id != 0) throw new ArgumentException("Id must be 0 while inserting");

            if (data.ContestId < 1)
                throw new ArgumentException("ContestId must not be less than 1");

            if (data.UserId < 1)
                throw new ArgumentException("UserId must not be less than 1");

            if (data.FirstId < 1)
                throw new ArgumentException("FirstId must not be less than 1");

            if (data.SecondId < 1)
                throw new ArgumentException("SecondId must not be less than 1");

            if (data.ThirdId < 1)
                throw new ArgumentException("ThirdId must not be less than 1");

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = InsertProcedure;
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add(new SqlParameter("@ContestId", data.ContestId));
            command.Parameters.Add(new SqlParameter("@FirstId", data.FirstId));
            command.Parameters.Add(new SqlParameter("@SecondId", data.SecondId));
            command.Parameters.Add(new SqlParameter("@ThirdId", data.ThirdId));
            command.Parameters.Add(new SqlParameter("@UserId", data.UserId));
            command.ExecuteNonQuery();
            return data.Id = Convert.ToInt32(command.Parameters["@Id"].Value);
        }

        /// <inheritdoc />
        public bool Update(VoteInfo data, long updateParamsLong = (long)VoteInfoParams.None)
        {
            var updateParams = (VoteInfoParams)updateParamsLong;
            if (data.Id < 1)
                throw new ArgumentException("Database Id must not be less than 1");

            if ((VoteInfoParams.ContestId & updateParams) == VoteInfoParams.ContestId && data.ContestId < 1)
                throw new ArgumentException("ContestId must not be less than 1");

            if ((VoteInfoParams.UserId & updateParams) == VoteInfoParams.UserId && data.UserId < 1)
                throw new ArgumentException("UserId must not be less than 1");

            if ((VoteInfoParams.FirstId & updateParams) == VoteInfoParams.FirstId && data.FirstId < 1)
                throw new ArgumentException("FirstId must not be less than 1");

            if ((VoteInfoParams.SecondId & updateParams) == VoteInfoParams.SecondId && data.SecondId < 1)
                throw new ArgumentException("SecondId must not be less than 1");

            if ((VoteInfoParams.ThirdId & updateParams) == VoteInfoParams.ThirdId && data.ThirdId < 1)
                throw new ArgumentException("ThirdId must not be less than 1");

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = UpdateProcedure;
            command.Parameters.Add(new SqlParameter("@Id", data.Id));
            command.Parameters.Add(new SqlParameter("@ContestId", data.ContestId));
            command.Parameters.Add(new SqlParameter("@UpdateContestId", VoteInfoParams.ContestId & updateParams));
            command.Parameters.Add(new SqlParameter("@FirstId", data.FirstId));
            command.Parameters.Add(new SqlParameter("@UpdateFirstId", VoteInfoParams.FirstId & updateParams));
            command.Parameters.Add(new SqlParameter("@SecondId", data.SecondId));
            command.Parameters.Add(new SqlParameter("@UpdateSecondId", VoteInfoParams.SecondId & updateParams));
            command.Parameters.Add(new SqlParameter("@ThirdId", data.ThirdId));
            command.Parameters.Add(new SqlParameter("@UpdateThirdId", VoteInfoParams.ThirdId & updateParams));
            command.Parameters.Add(new SqlParameter("@UserId", data.UserId));
            command.Parameters.Add(new SqlParameter("@UpdateUserId", VoteInfoParams.UserId & updateParams));
            return command.ExecuteNonQuery() > 0;
        }

        /// <inheritdoc />
        public bool Delete(int id)
        {
            if (id < 1)
                throw new ArgumentException("Database Id must not be less than 1");

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = DeleteProcedure;
            command.Parameters.Add(new SqlParameter("@Id", id));
            return command.ExecuteNonQuery() > 0;
        }

        private static VoteInfo ParseData(System.Data.IDataRecord record)
        {
            if (record is null)
                throw new ArgumentNullException(nameof(record));

            return new VoteInfo
            {
                Id = (int)record["Id"],
                UserId = (int)record["UserId"],
                ContestId = (int)record["ContestId"],
                FirstId = (int)record["FirstId"],
                SecondId = (int)record["SecondId"],
                ThirdId = (int)record["ThirdId"]
            };
        }
    }
}
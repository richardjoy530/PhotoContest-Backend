#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PhotoContest.Implementation.Ado.DataRecords;

#endregion

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
///     Database access layer of <see cref="Models.UserInfo" />
/// </summary>
public class UserInfoProvider : IProvider<UserInfo>
{
    private const string GetByIdProcedure = "[dbo].[UserInfo_GetById]";
    private const string GetProcedure = "[dbo].[UserInfo_GetAll]";
    private const string GetAllIdsProcedure = "[dbo].[UserInfo_GetAllIds]";
    private const string InsertProcedure = "[dbo].[UserInfo_Insert]";
    private const string UpdateProcedure = "[dbo].[UserInfo_Update]";
    private const string DeleteProcedure = "[dbo].[UserInfo_Delete]";
    private readonly string _connectionString;

    /// <summary>
    ///     Initializes a new instance of UserInfoProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public UserInfoProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    /// <inheritdoc />
    public int Insert(UserInfo data)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        if (data.Id != 0) throw new ArgumentException("Id must be 0 while inserting");

        if (string.IsNullOrWhiteSpace(data.Name))
            throw new ArgumentException($"{nameof(data.Name)} is null or empty");

        if (string.IsNullOrWhiteSpace(data.Email))
            throw new ArgumentException($"{nameof(data.Email)} is null or empty");

        if (string.IsNullOrWhiteSpace(data.RefId))
            data.RefId = Guid.NewGuid().ToString();

        if (data.RegistrationDate == default)
            data.RegistrationDate = DateTime.Now;

        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@Name", data.Name));
        command.Parameters.Add(new SqlParameter("@Email", data.Email));
        command.Parameters.Add(new SqlParameter("@RegistrationDate", data.RegistrationDate));
        command.Parameters.Add(new SqlParameter("@RefId", data.RefId));
        command.ExecuteNonQuery();

        return data.Id = Convert.ToInt32(command.Parameters["@Id"].Value);
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

    /// <inheritdoc />
    public UserInfo GetById(int id)
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
    public IEnumerable<UserInfo> GetAll()
    {
        var userInfos = new List<UserInfo>();
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure;
        using var reader = command.ExecuteReader();
        while (reader.Read())
            userInfos.Add(ParseData(reader));

        return userInfos;
    }

    /// <inheritdoc />
    public bool Update(UserInfo data, long updateParamsLong = (long)UserInfoParams.None)
    {
        var updateParams = (UserInfoParams)updateParamsLong;

        if (data.Id < 1)
            throw new ArgumentException("Database Id must not be less than 1");

        if ((UserInfoParams.Name & updateParams) == UserInfoParams.Name && string.IsNullOrWhiteSpace(data.Name))
            throw new ArgumentException("Name must not be null or whitespace");

        if ((UserInfoParams.Email & updateParams) == UserInfoParams.Email && string.IsNullOrWhiteSpace(data.Email))
            throw new ArgumentException("Email must not be null or whitespace");

        if ((UserInfoParams.RefId & updateParams) == UserInfoParams.RefId && string.IsNullOrWhiteSpace(data.RefId))
            throw new ArgumentException("RefId must not be null or whitespace");

        if ((UserInfoParams.RegistrationDate & updateParams) == UserInfoParams.RegistrationDate &&
            data.RegistrationDate == default)
            throw new ArgumentException("RegistrationDate must be valid date.");

        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", data.Id));
        command.Parameters.Add(new SqlParameter("@Name", data.Name));
        command.Parameters.Add(new SqlParameter("@UpdateName", UserInfoParams.Name & updateParams));
        command.Parameters.Add(new SqlParameter("@Email", data.Email));
        command.Parameters.Add(new SqlParameter("@UpdateEmail", UserInfoParams.Email & updateParams));
        command.Parameters.Add(new SqlParameter("@RefId", data.RefId));
        command.Parameters.Add(new SqlParameter("@UpdateRefId", UserInfoParams.RefId & updateParams));
        command.Parameters.Add(new SqlParameter("@RegistrationDate", data.RegistrationDate));
        command.Parameters.Add(new SqlParameter("@UpdateRegistrationDate",
            UserInfoParams.RegistrationDate & updateParams));
        return command.ExecuteNonQuery() > 0;
    }

    private static UserInfo ParseData(System.Data.IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));

        return new UserInfo
        {
            Id = (int)record["Id"],
            Name = (string)record["Name"],
            Email = (string)record["Email"],
            RefId = (string)record["RefId"],
            RegistrationDate = (DateTime)record["RegistrationDate"]
        };
    }
}
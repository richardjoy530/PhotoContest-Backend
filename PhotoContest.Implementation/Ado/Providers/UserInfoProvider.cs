#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
///     Database access layer of <see cref="Models.UserInfo" />
/// </summary>
public class UserInfoProvider : IProvider<UserInfo>
{
    private readonly string _connectionString;
    private const string GetByIdProcedure = "[dbo].[UserInfo_GetById]";
    private const string GetProcedure = "[dbo].[UserInfo_GetAll]";
    private const string InsertProcedure = "[dbo].[UserInfo_Insert]";
    private const string UpdateProcedure = "[dbo].[UserInfo_Update]";
    private const string DeleteProcedure = "[dbo].[UserInfo_Delete]";

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
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@Name", data.Name));
        command.ExecuteNonQuery();

        return Convert.ToInt32(command.Parameters["@Id"].Value);
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = DeleteProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.ExecuteNonQuery();
    }

    /// <inheritdoc />
    public UserInfo GetById(int id)
    {
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
    public void Update(UserInfo data, int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@Name", data.Name));
        command.ExecuteNonQuery();
    }

    private static UserInfo ParseData(IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));
        
        return new UserInfo()
        {
            Id = (int)record["Id"],
            Name = (string)record["Name"],
        };
    }
}
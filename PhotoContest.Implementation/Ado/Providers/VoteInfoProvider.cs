#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
///     Database access layer of <see cref="VoteInfo" />
/// </summary>
public class VoteInfoProvider : IProvider<VoteInfo>
{
    private readonly string _connectionString;
    private const string GetByIdProcedure = "[dbo].[VoteInfo_GetById]";
    private const string GetProcedure = "[dbo].[VoteInfo_GetAll]";
    private const string InsertProcedure = "[dbo].[VoteInfo_Insert]";
    private const string UpdateProcedure = "[dbo].[VoteInfo_Update]";
    private const string DeleteProcedure = "[dbo].[VoteInfo_Delete]";

    /// <summary>
    ///     Initializes a new instance of PhotoEntryProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public VoteInfoProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    /// <inheritdoc />
    public VoteInfo GetById(int id)
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
    public IEnumerable<VoteInfo> GetAll()
    {
        var photoEntries = new List<VoteInfo>();
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure;
        using var reader = command.ExecuteReader();
        while (reader.Read())
            photoEntries.Add(ParseData(reader));

        return photoEntries;
    }

    /// <inheritdoc />
    public int Insert(VoteInfo data)
    {
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
        return Convert.ToInt32(command.Parameters["@Id"].Value);
    }

    /// <inheritdoc />
    public void Update(VoteInfo data, int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@ContestId", data.ContestId));
        command.Parameters.Add(new SqlParameter("@FirstId", data.FirstId));
        command.Parameters.Add(new SqlParameter("@SecondId", data.SecondId));
        command.Parameters.Add(new SqlParameter("@ThirdId", data.ThirdId));
        command.Parameters.Add(new SqlParameter("@UserId", data.UserId));
        command.ExecuteNonQuery();
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

    private static VoteInfo ParseData(IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));
        
        return new VoteInfo()
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
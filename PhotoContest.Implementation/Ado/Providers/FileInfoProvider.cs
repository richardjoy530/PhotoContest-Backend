#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
///     Database access layer of <see cref="Contest" />
/// </summary>
public class FileInfoProvider : IProvider<FileInfo>
{
    private readonly string _connectionString;
    private const string GetByIdProcedure = "[dbo].[FileInfo_GetById]";
    private const string GetProcedure = "[dbo].[FileInfo_GetAll]";
    private const string InsertProcedure = "[dbo].[FileInfo_Insert]";
    private const string UpdateProcedure = "[dbo].[FileInfo_Update]";
    private const string DeleteProcedure = "[dbo].[FileInfo_Delete]";

    /// <summary>
    ///     Initializes a new instance of ContestProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public FileInfoProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    /// <inheritdoc />
    public int Insert(FileInfo data)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@Path", data.Path));
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
    public FileInfo GetById(int id)
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
    public void Update(FileInfo data, int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@Path", data.Path));
        command.ExecuteNonQuery();
    }

    private static FileInfo ParseData(System.Data.IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));
        
        return new FileInfo()
        {
            Path = (string)record["Path"],
            Id = (int)record["Id"]
        };
    }
}
#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
///     Database access layer of <see cref="Submission" />
/// </summary>
public class SubmissionProvider : IProvider<Submission>
{
    private readonly string _connectionString;
    private const string GetByIdProcedure = "[dbo].[Submission_GetById]";
    private const string GetProcedure = "[dbo].[Submission_GetAll]";
    private const string InsertProcedure = "[dbo].[Submission_Insert]";
    private const string UpdateProcedure = "[dbo].[Submission_Update]";
    private const string DeleteProcedure = "[dbo].[Submission_Delete]";

    /// <summary>
    ///     Initializes a new instance of PhotoEntryProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public SubmissionProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    /// <inheritdoc />
    public Submission GetById(int id)
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
    public IEnumerable<Submission> GetAll()
    {
        var photoEntries = new List<Submission>();
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
    public int Insert(Submission data)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@ContestId", data.ContestId));
        command.Parameters.Add(new SqlParameter("@FileInfoId", data.FileInfoId));
        command.Parameters.Add(new SqlParameter("@Caption", data.Caption));
        command.Parameters.Add(new SqlParameter("@UserId", data.UserId));
        command.Parameters.Add(new SqlParameter("@UploadedOn", data.UploadedOn));
        command.ExecuteNonQuery();
        return Convert.ToInt32(command.Parameters["@Id"].Value);
    }

    /// <inheritdoc />
    public void Update(Submission data, int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@ContestId", data.ContestId));
        command.Parameters.Add(new SqlParameter("@FileInfoId", data.FileInfoId));
        command.Parameters.Add(new SqlParameter("@Caption", data.Caption));
        command.Parameters.Add(new SqlParameter("@UserId", data.UserId));
        command.Parameters.Add(new SqlParameter("@UploadedOn", data.UploadedOn));
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

    private static Submission ParseData(IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));
        
        return new Submission()
        {
            Id = (int)record["Id"],
            UserId = (int)record["UserId"],
            Caption = (string)record["Caption"],
            ContestId = (int)record["ContestId"],
            FileInfoId = (int)record["FileInfoId"],
            UploadedOn = (DateTime)record["UploadedOn"]
        };
    }
}
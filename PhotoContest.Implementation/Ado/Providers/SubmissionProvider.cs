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
    public IEnumerable<Submission> GetAll()
    {
        var data = new List<Submission>();
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
    public int Insert(Submission data)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        if (data.Id != 0) throw new ArgumentException("Id must be 0 while inserting");

        if (data.ContestId < 1)
            throw new ArgumentException("ContestId must not be less than 1");
        
        if (data.UserId < 1)
            throw new ArgumentException("UserId must not be less than 1");
        
        if (data.FileInfoId < 1)
            throw new ArgumentException("FileInfoId must not be less than 1");
        
        if (string.IsNullOrWhiteSpace(data.Caption))
            throw new ArgumentException("Caption must not be null or whitespace");

        if (string.IsNullOrWhiteSpace(data.RefId))
            data.RefId = Guid.NewGuid().ToString();
        
        if (data.UploadedOn == default)
            data.UploadedOn = DateTime.Now;
        
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
        command.Parameters.Add(new SqlParameter("@RefId", data.RefId));
        command.ExecuteNonQuery();
        
        return data.Id = Convert.ToInt32(command.Parameters["@Id"].Value);
    }

    /// <inheritdoc />
    public bool Update(Submission data, long updateParamsLong = (long)SubmissionParams.None)
    {
        var updateParams = (SubmissionParams)updateParamsLong;
        if (data.Id < 1) 
            throw new ArgumentException("Database Id must not be less than 1");

        if ((SubmissionParams.ContestId & updateParams) == SubmissionParams.ContestId && data.ContestId < 1)
            throw new ArgumentException("ContestId must not be less than 1");
        
        if ((SubmissionParams.UserId & updateParams) == SubmissionParams.UserId && data.UserId < 1)
            throw new ArgumentException("UserId must not be less than 1");
        
        if ((SubmissionParams.FileInfoId & updateParams) == SubmissionParams.FileInfoId && data.FileInfoId < 1)
            throw new ArgumentException("FileInfoId must not be less than 1");
        
        if ((SubmissionParams.Caption & updateParams) == SubmissionParams.Caption && string.IsNullOrWhiteSpace(data.Caption))
            throw new ArgumentException("Caption must not be null or whitespace");
        
        if ((SubmissionParams.RefId & updateParams) == SubmissionParams.RefId && string.IsNullOrWhiteSpace(data.RefId))
            throw new ArgumentException("RefId must not be null or whitespace");
        
        if ((SubmissionParams.UploadedOn & updateParams) == SubmissionParams.UploadedOn && data.UploadedOn == default)
            throw new ArgumentException("UploadedOn must be valid date.");
        
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", data.Id));
        command.Parameters.Add(new SqlParameter("@ContestId", data.ContestId));
        command.Parameters.Add(new SqlParameter("@UpdateContestId", SubmissionParams.ContestId & updateParams));
        command.Parameters.Add(new SqlParameter("@FileInfoId", data.FileInfoId));
        command.Parameters.Add(new SqlParameter("@UpdateFileInfoId", SubmissionParams.FileInfoId & updateParams));
        command.Parameters.Add(new SqlParameter("@Caption", data.Caption));
        command.Parameters.Add(new SqlParameter("@UpdateCaption", SubmissionParams.Caption & updateParams));
        command.Parameters.Add(new SqlParameter("@UserId", data.UserId));
        command.Parameters.Add(new SqlParameter("@UpdateUserId", SubmissionParams.UserId & updateParams));
        command.Parameters.Add(new SqlParameter("@UploadedOn", data.UploadedOn));
        command.Parameters.Add(new SqlParameter("@UpdateUploadedOn", SubmissionParams.UploadedOn & updateParams));
        command.Parameters.Add(new SqlParameter("@RefId", data.RefId));
        command.Parameters.Add(new SqlParameter("@UpdateRefId", SubmissionParams.RefId & updateParams));
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

    private static Submission ParseData(System.Data.IDataRecord record)
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
            UploadedOn = (DateTime)record["UploadedOn"],
            RefId = (string)record["RefId"],
        };
    }
}
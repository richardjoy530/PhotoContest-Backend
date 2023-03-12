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
public class ScoreInfoProvider : IProvider<ScoreInfo>
{
    private readonly string _connectionString;
    private const string GetByIdProcedure = "[dbo].[ScoreInfo_GetById]";
    private const string GetProcedure = "[dbo].[ScoreInfo_GetAll]";
    private const string InsertProcedure = "[dbo].[ScoreInfo_Insert]";
    private const string UpdateProcedure = "[dbo].[ScoreInfo_Update]";
    private const string DeleteProcedure = "[dbo].[ScoreInfo_Delete]";

    /// <summary>
    ///     Initializes a new instance of ContestProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public ScoreInfoProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    /// <inheritdoc />
    public int Insert(ScoreInfo data)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@Score", data.Score));
        command.Parameters.Add(new SqlParameter("@SubmissionId", data.SubmissionId));
        command.ExecuteNonQuery();
        return Convert.ToInt32(command.Parameters["@Id"].Value);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = DeleteProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        return command.ExecuteNonQuery() > 0;
    }

    /// <inheritdoc />
    public ScoreInfo GetById(int id)
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
    public IEnumerable<ScoreInfo> GetAll()
    {
        var scoreInfos = new List<ScoreInfo>();
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure;
        using var reader = command.ExecuteReader();
        while (reader.Read())
            scoreInfos.Add(ParseData(reader));

        return scoreInfos;
    }

    /// <inheritdoc />
    public void Update(ScoreInfo data, int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@Path", data.Score));
        command.Parameters.Add(new SqlParameter("@SubmissionId", data.SubmissionId));
        command.ExecuteNonQuery();
    }

    private static ScoreInfo ParseData(System.Data.IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));
        
        return new ScoreInfo()
        {
            Id = (int)record["Id"],
            Score = (int)record["Score"],
            SubmissionId = (int)record["SubmissionId"],
        };
    }
}
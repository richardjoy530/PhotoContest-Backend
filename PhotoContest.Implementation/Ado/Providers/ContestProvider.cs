﻿#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
///     Database access layer of <see cref="Contest" />
/// </summary>
public class ContestProvider : IProvider<Contest>
{
    private readonly string _connectionString;
    private const string GetByIdProcedure = "[dbo].[Contest_GetById]";
    private const string GetProcedure = "[dbo].[Contest_GetAll]";
    private const string InsertProcedure = "[dbo].[Contest_Insert]";
    private const string UpdateProcedure = "[dbo].[Contest_Update]";
    private const string DeleteProcedure = "[dbo].[Contest_Delete]";

    /// <summary>
    ///     Initializes a new instance of ContestProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public ContestProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    /// <inheritdoc />
    public int Insert(Contest data)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@Theme", data.Theme));
        command.Parameters.Add(new SqlParameter("@EndDate", data.EndDate));
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
    public Contest GetById(int id)
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
    public IEnumerable<Contest> GetAll()
    {
        var contests = new List<Contest>();
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
    public void Update(Contest data, int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@Theme", data.Theme));
        command.Parameters.Add(new SqlParameter("@EndDate", data.EndDate));
        command.ExecuteNonQuery();
    }

    private static Contest ParseData(IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));
        
        return new Contest()
        {
            EndDate = (DateTime)record["EndDate"],
            Theme = (string)record["Theme"],
            Id = (int)record["Id"]
        };
    }
}
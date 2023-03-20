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
///     Database access layer of <see cref="Contest" />
/// </summary>
public class ContestProvider : IProvider<Contest>
{
    private const string GetByIdProcedure = "[dbo].[Contest_GetById]";
    private const string GetProcedure = "[dbo].[Contest_GetAll]";
    private const string InsertProcedure = "[dbo].[Contest_Insert]";
    private const string UpdateProcedure = "[dbo].[Contest_Update]";
    private const string DeleteProcedure = "[dbo].[Contest_Delete]";
    private const string GetAllIdsProcedure = "[dbo].[Contest_GetAllIds]";
    private readonly string _connectionString;

    /// <summary>
    ///     Initializes a new instance of ContestProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public ContestProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    // todo: validate end date not colliding withe the existing
    /// <inheritdoc />
    public int Insert(Contest data)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        if (data.Id != 0) throw new ArgumentException("Id must be 0 while inserting");

        if (data.EndDate == default)
            throw new ArgumentException($"{nameof(data.EndDate)} is invalid current value is {data.EndDate}");

        if (string.IsNullOrWhiteSpace(data.Theme))
            throw new ArgumentException($"{nameof(data.Theme)} is null or empty");

        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@Theme", data.Theme));
        command.Parameters.Add(new SqlParameter("@EndDate", data.EndDate));
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
    public Contest GetById(int id)
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
    public bool Update(Contest data, long updateParamsLong = (long)ContestParams.None)
    {
        var updateParams = (ContestParams)updateParamsLong;
        if (data is null) throw new ArgumentNullException(nameof(data));

        if (data.Id < 1) throw new ArgumentException("Database Id must not be less than 1");

        if ((ContestParams.EndDate & updateParams) == ContestParams.EndDate && data.EndDate == default)
            throw new ArgumentException($"{nameof(data.EndDate)} is invalid current value is {data.EndDate}");

        if ((ContestParams.Theme & updateParams) == ContestParams.Theme && string.IsNullOrWhiteSpace(data.Theme))
            throw new ArgumentException($"{nameof(data.Theme)} is null or empty");

        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", data.Id));
        command.Parameters.Add(new SqlParameter("@Theme", data.Theme));
        command.Parameters.Add(new SqlParameter("@UpdateTheme", ContestParams.Theme & updateParams));
        command.Parameters.Add(new SqlParameter("@EndDate", data.EndDate));
        command.Parameters.Add(new SqlParameter("@UpdateEndDate", ContestParams.EndDate & updateParams));
        return command.ExecuteNonQuery() > 0;
    }

    private static Contest ParseData(System.Data.IDataRecord record)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));

        return new Contest
        {
            EndDate = (DateTime)record["EndDate"],
            Theme = (string)record["Theme"],
            Id = (int)record["Id"]
        };
    }
}
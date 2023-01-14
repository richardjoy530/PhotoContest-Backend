#region

using System;
using System.Data;
using System.Data.SqlClient;
using PhotoContest.Models;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
///     Maps reference id to integer id from database.
/// </summary>
public class ReferenceIdProvider : IReferenceIdMapper
{
    private const string InsertProcedure = "[dbo].[Insert_IdMap]";
    private const string GetIdProcedure = "[dbo].[GetId_IdMap]";
    private const string GetRefProcedure = "[dbo].[GetRef_IdMap]";
    private readonly string _connectionString;

    /// <summary>
    ///     Initializes a new instance of ReferenceIdProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    public ReferenceIdProvider(IDbConnection dbConnection)
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
    }

    /// <inheritdoc />
    public int GetIntegerId(string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetIdProcedure;
        command.Parameters.Add(new SqlParameter("@ReferenceId", referenceId));
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.ExecuteNonQuery();
        return Convert.ToInt32(command.Parameters["@Id"].Value);
    }

    /// <inheritdoc />
    public string GetReferenceId(int id, IdType idType)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetRefProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@IdType", (int) idType));
        command.Parameters.Add("@ReferenceId", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
        command.ExecuteNonQuery();
        return command.Parameters["@ReferenceId"].Value.ToString();
    }

    /// <inheritdoc />
    public void InsertIdMap(Id id, IdType idType)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add(new SqlParameter("@Id", id.IntegerId));
        command.Parameters.Add(new SqlParameter("@ReferenceId", id.ReferenceId));
        command.Parameters.Add(new SqlParameter("@IdType", (int) idType));
        command.ExecuteNonQuery();
    }
}
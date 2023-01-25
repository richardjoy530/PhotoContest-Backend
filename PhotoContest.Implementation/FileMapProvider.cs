#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PhotoContest.Models;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
/// </summary>
public class FileMapProvider : IProvider<FileMap>
{
    private const string InsertProcedure = "[dbo].[Insert_FileMap]";
    private const string GetProcedure = "[dbo].[Get_FileMap]";
    private const string UpdateProcedure = "[dbo].[Update_FileMap]";
    private const string DeleteProcedure = "[dbo].[Delete_FileMap]";
    private readonly string _connectionString;
    private readonly IReferenceIdMapper _referenceIdMapper;

    /// <summary>
    /// </summary>
    public FileMapProvider(
        IDbConnection dbConnection,
        IReferenceIdMapper referenceIdMapper
    )
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));
        _connectionString = dbConnection.ConnectionString;
        _referenceIdMapper = referenceIdMapper ?? throw new ArgumentNullException(nameof(referenceIdMapper));
    }

    /// <inheritdoc />
    public FileMap Insert(FileMap fileMap)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@FilePath", fileMap.FilePath));
        command.ExecuteNonQuery();
        fileMap.Id.IntegerId = Convert.ToInt32(command.Parameters["@Id"].Value);
        return fileMap;
    }

    /// <inheritdoc />
    public void Delete(string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = DeleteProcedure;
        command.Parameters.Add(new SqlParameter("@Id", _referenceIdMapper.GetIntegerId(referenceId)));
        command.ExecuteNonQuery();
    }

    /// <inheritdoc />
    public IEnumerable<FileMap> GetAll()
    {
        var fileMaps = new List<FileMap>();
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var fileMap = new FileMap(reader);
            fileMap.ResolveReferenceId(_referenceIdMapper);
            fileMaps.Add(fileMap);
        }

        return fileMaps;
    }

    /// <inheritdoc />
    public FileMap GetById(string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure;
        command.Parameters.Add(new SqlParameter("@Id", _referenceIdMapper.GetIntegerId(referenceId)));
        using var reader = command.ExecuteReader();
        reader.Read();
        var fileMap = new FileMap(reader);
        return fileMap;
    }

    /// <inheritdoc />
    public void Update(FileMap fileMap, string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", _referenceIdMapper.GetIntegerId(referenceId)));
        command.Parameters.Add(new SqlParameter("@FilePath", fileMap.FilePath));
        command.ExecuteNonQuery();
    }
}
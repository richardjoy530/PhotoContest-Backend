#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PhotoContest.Models;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
///     Database access layer of <see cref="PhotoTheme" />
/// </summary>
public class PhotoThemeProvider : IProvider<PhotoTheme>
{
    private readonly string _connectionString;
    private const string DeleteProcedure = "[dbo].[Delete_Photographer]";
    private const string GetByIdProcedure = "[dbo].[GetById_Photographer]";
    private const string GetProcedure = "[dbo].[Get_Photographer]";
    private const string InsertProcedure = "[dbo].[Insert_Photographer]";
    private readonly IReferenceIdMapper _referenceIdMapper;
    private const string UpdateProcedure = "[dbo].[Update_Photographer]";

    /// <summary>
    ///     Initializes a new instance of PhotoThemeProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    /// <param name="referenceIdMapper"></param>
    public PhotoThemeProvider(
        IDbConnection dbConnection,
        IReferenceIdMapper referenceIdMapper
    )
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
        _referenceIdMapper = referenceIdMapper ?? throw new ArgumentNullException(nameof(referenceIdMapper));
    }

    /// <inheritdoc />
    public PhotoTheme Insert(PhotoTheme photographer)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
        command.Parameters.Add(new SqlParameter("@Theme", photographer.Theme));
        command.Parameters.Add(new SqlParameter("@ContestDate", photographer.ContestDate));
        command.ExecuteNonQuery();
        photographer.Id.IntegerId = Convert.ToInt32(command.Parameters["@Id"].Value);

        return photographer;
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
    public PhotoTheme GetById(string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetByIdProcedure;
        command.Parameters.Add(new SqlParameter("@Id", _referenceIdMapper.GetIntegerId(referenceId)));
        using var reader = command.ExecuteReader();
        reader.Read();
        var photographer = new PhotoTheme(reader);

        return photographer;
    }

    /// <inheritdoc />
    public IEnumerable<PhotoTheme> GetAll()
    {
        var photographers = new List<PhotoTheme>();
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var photographer = new PhotoTheme(reader);
            photographer.ResolveReferenceId(_referenceIdMapper);
            photographers.Add(photographer);
        }

        return photographers;
    }

    /// <inheritdoc />
    public void Update(PhotoTheme photographer, string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", _referenceIdMapper.GetIntegerId(referenceId)));
        command.Parameters.Add(new SqlParameter("@Theme", photographer.Theme));
        command.Parameters.Add(new SqlParameter("@ContestDate", photographer.ContestDate));
        command.ExecuteNonQuery();
    }
}
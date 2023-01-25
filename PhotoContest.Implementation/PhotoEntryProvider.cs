#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PhotoContest.Models;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
///     Database access layer of <see cref="PhotoEntry" />
/// </summary>
public class PhotoEntryProvider : IProvider<PhotoEntry>
{
    private readonly string _connectionString;
    private const string DeleteProcedure = "[dbo].[Delete_PhotoEntry]";
    private const string GetByIdProcedure = "[dbo].[GetById_PhotoEntry]";
    private const string GetProcedure = "[dbo].[Get_PhotoEntry]";
    private const string InsertProcedure = "[dbo].[Insert_PhotoEntry]";
    private readonly IReferenceIdMapper _referenceIdMapper;
    private const string UpdateProcedure = "[dbo].[Update_PhotoEntry]";

    /// <summary>
    ///     Initializes a new instance of PhotoEntryProvider class
    /// </summary>
    /// <param name="dbConnection"></param>
    /// <param name="referenceIdMapper"></param>
    public PhotoEntryProvider(
        IDbConnection dbConnection,
        IReferenceIdMapper referenceIdMapper
    )
    {
        if (dbConnection is null) throw new ArgumentNullException(nameof(dbConnection));

        _connectionString = dbConnection.ConnectionString;
        _referenceIdMapper = referenceIdMapper ?? throw new ArgumentNullException(nameof(referenceIdMapper));
    }

    /// <inheritdoc />
    public PhotoEntry GetById(string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetByIdProcedure;
        command.Parameters.Add(new SqlParameter("@Id", _referenceIdMapper.GetIntegerId(referenceId)));
        using var reader = command.ExecuteReader();
        reader.Read();
        var photoEntry = new PhotoEntry(reader);

        return photoEntry;
    }

    /// <inheritdoc />
    public IEnumerable<PhotoEntry> GetAll()
    {
        var photoEntries = new List<PhotoEntry>();
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var photoEntry = new PhotoEntry(reader);
            photoEntry.ResolveReferenceId(_referenceIdMapper);
            photoEntries.Add(photoEntry);
        }

        return photoEntries;
    }

    /// <inheritdoc />
    public PhotoEntry Insert(PhotoEntry photoEntry)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = InsertProcedure;
        command.Parameters.Add("@Id", SqlDbType.Int).Direction =
            ParameterDirection.Output; //Getting value from DB without querying
        command.Parameters.Add(new SqlParameter("@ThemeId", photoEntry.Theme.Id.IntegerId));
        command.Parameters.Add(new SqlParameter("@FileId", photoEntry.FileId.IntegerId));
        command.Parameters.Add(new SqlParameter("@Caption", photoEntry.Caption));
        command.Parameters.Add(new SqlParameter("@PhotographerId", photoEntry.Photographer.Id.IntegerId));
        command.Parameters.Add(new SqlParameter("@UploadedOn", photoEntry.UploadedOn));
        command.ExecuteNonQuery();
        photoEntry.Id.IntegerId =
            Convert.ToInt32(command.Parameters["@Id"].Value); //Getting value from DB without querying

        return photoEntry;
    }

    /// <inheritdoc />
    public void Update(PhotoEntry photoEntry, string referenceId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", _referenceIdMapper.GetIntegerId(referenceId)));
        command.Parameters.Add(new SqlParameter("@Theme", photoEntry.Theme));
        command.Parameters.Add(new SqlParameter("@FileId", photoEntry.FileId.IntegerId));
        command.Parameters.Add(new SqlParameter("@Caption", photoEntry.Caption));
        command.Parameters.Add(new SqlParameter("@PhotographerId", photoEntry.Photographer.Id.IntegerId));
        command.Parameters.Add(new SqlParameter("@UploadedOn", photoEntry.UploadedOn));
        command.ExecuteNonQuery();
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

    /// <summary>
    ///     Retreves a list of all the <see cref="PhotoEntry" /> in database with the given theme
    /// </summary>
    /// <param name="theme" />
    /// <returns>Enumerable of <see cref="PhotoEntry" /></returns>
#pragma warning disable IDE0051 // Remove unused private members
    private IEnumerable<PhotoEntry> GetPhotoEntries(string theme)
#pragma warning restore IDE0051 // Remove unused private members
    {
        var photoEntries = new List<PhotoEntry>();
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = GetProcedure; // TODO: bitmask conditions in get
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var photoEntry = new PhotoEntry(reader);
            photoEntry.ResolveReferenceId(_referenceIdMapper);
            photoEntries.Add(photoEntry);
        }

        return photoEntries;
    }
}
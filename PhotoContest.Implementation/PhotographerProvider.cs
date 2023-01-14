#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PhotoContest.Models;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
///     Database access layer of <see cref="Photographer" />
/// </summary>
public class PhotographerProvider : IProvider<Photographer>
{
    private readonly string connectionString;
    private readonly string DeleteProcedure = "[dbo].[Delete_Photographer]";
    private readonly string GetByIdProcedure = "[dbo].[GetById_Photographer]";
    private readonly string GetProcedure = "[dbo].[Get_Photographer]";
    private readonly string InsertProcedure = "[dbo].[Insert_Photographer]";
    private readonly IReferenceIdMapper referenceIdMapper;
    private readonly string UpdateProcedure = "[dbo].[Update_Photographer]";

    /// <summary>
    ///     Initializes a new instance of PhotographerProvider class
    /// </summary>
    /// <param name="_dbConnection"></param>
    /// <param name="_referenceIdMapper"></param>
    public PhotographerProvider(
        IDbConnection _dbConnection,
        IReferenceIdMapper _referenceIdMapper
    )
    {
        if (_dbConnection is null) throw new ArgumentNullException(nameof(_dbConnection));

        connectionString = _dbConnection.ConnectionString;
        referenceIdMapper = _referenceIdMapper ?? throw new ArgumentNullException(nameof(_referenceIdMapper));
    }

    /// <inheritdoc />
    public Photographer Insert(Photographer photographer)
    {
        using (SqlConnection conncetion = new(connectionString))
        {
            conncetion.Open();
            using var command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = InsertProcedure;
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add(new SqlParameter("@UploaderName", photographer.UploaderName));
            command.ExecuteNonQuery();
            photographer.Id.IntegerId = Convert.ToInt32(command.Parameters["@Id"].Value);
        }

        return photographer;
    }

    /// <inheritdoc />
    public void Delete(string referenceId)
    {
        using SqlConnection conncetion = new(connectionString);
        conncetion.Open();
        using var command = conncetion.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = DeleteProcedure;
        command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
        command.ExecuteNonQuery();
    }

    /// <inheritdoc />
    public Photographer GetById(string referenceId)
    {
        Photographer photographer;
        using (SqlConnection conncetion = new(connectionString))
        {
            conncetion.Open();
            using var command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetByIdProcedure;
            command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
            using var reader = command.ExecuteReader();
            reader.Read();
            photographer = new Photographer(reader);
        }

        return photographer;
    }

    /// <inheritdoc />
    public IEnumerable<Photographer> GetAll()
    {
        var photographers = new List<Photographer>();
        using (SqlConnection conncetion = new(connectionString))
        {
            conncetion.Open();
            using var command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = GetProcedure;
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var photographer = new Photographer(reader);
                photographer.ResolveReferenceId(referenceIdMapper);
                photographers.Add(photographer);
            }
        }

        return photographers;
    }

    /// <inheritdoc />
    public void Update(Photographer photographer, string referenceId)
    {
        using SqlConnection conncetion = new(connectionString);
        conncetion.Open();
        using var command = conncetion.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = UpdateProcedure;
        command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceId)));
        command.Parameters.Add(new SqlParameter("@UploaderName", photographer.UploaderName));
        command.ExecuteNonQuery();
    }
}
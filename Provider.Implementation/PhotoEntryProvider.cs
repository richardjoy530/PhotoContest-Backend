using Provider.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Provider.Implementation
{
    /// <summary>
    /// Database access layer of <see cref="PhotoEntry"/>
    /// </summary>
    public class PhotoEntryProvider : IProvider<PhotoEntry>
    {
        private readonly string connectionString;
        private readonly IReferenceIdMapper referenceIdMapper;
        private readonly string InsertProcedure = "[dbo].[Insert_PhotoEntry]";
        private readonly string GetProcedure    = "[dbo].[Get_PhotoEntry]";
        private readonly string GetAllProcedure = "[dbo].[GetAll_PhotoEntry]";
        private readonly string UpdateProcedure = "[dbo].[Update_PhotoEntry]";
        private readonly string DeleteProcedure = "[dbo].[Delete_PhotoEntry]";

        /// <summary>
        /// Initializes a new instance of PhotoEntryProvider class
        /// </summary>
        /// <param name="_dbConnection"></param>
        /// <param name="_referenceIdMapper"></param>
        public PhotoEntryProvider(
            IDbConnection _dbConnection,
            IReferenceIdMapper _referenceIdMapper
            )
        {
            if (_dbConnection is null)
            {
                throw new ArgumentNullException(nameof(_dbConnection));
            }

            connectionString = _dbConnection.ConnectionString;
            referenceIdMapper = _referenceIdMapper ?? throw new ArgumentNullException(nameof(_referenceIdMapper));
        }

        /// <inheritdoc />
        public PhotoEntry GetById(string referenceid)
        {
            PhotoEntry photoEntry;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetProcedure;
                command.Parameters.Add(new SqlParameter("@Id", 3));
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                photoEntry = new PhotoEntry(reader);
            }
            photoEntry.ResolveReferenceId(referenceIdMapper);
            return photoEntry;
        }

        /// <inheritdoc />
        public IEnumerable<PhotoEntry> GetAll()
        {
            var photoEntries = new List<PhotoEntry>();
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetAllProcedure;
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var photoEntry = new PhotoEntry(reader);
                    photoEntry.ResolveReferenceId(referenceIdMapper);
                    photoEntries.Add(photoEntry);
                }
            }
            return photoEntries;
        }

        /// <inheritdoc />
        public PhotoEntry Insert(PhotoEntry photoEntry)
        {
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = InsertProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output; //Getting value from DB without querying
                command.Parameters.Add(new SqlParameter("@ThemeId", photoEntry.Theme.Id.IntegerId));
                command.Parameters.Add(new SqlParameter("@FileId", photoEntry.FileId.IntegerId));
                command.Parameters.Add(new SqlParameter("@Caption", photoEntry.Caption));
                command.Parameters.Add(new SqlParameter("@PhotographerId", photoEntry.Photographer.Id.IntegerId));
                command.Parameters.Add(new SqlParameter("@UploadedOn", photoEntry.UploadedOn));
                command.ExecuteNonQuery();
                photoEntry.Id.IntegerId = Convert.ToInt32(command.Parameters["@Id"].Value); //Getting value from DB without querying
            }
            return photoEntry;
        }

        /// <inheritdoc />
        public PhotoEntry Update(PhotoEntry photoEntry, string referenceId)
        {
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = UpdateProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output; //Getting value from DB without querying
                command.Parameters.Add(new SqlParameter("@Theme", photoEntry.Theme));
                command.Parameters.Add(new SqlParameter("@FileId", photoEntry.FileId.IntegerId));
                command.Parameters.Add(new SqlParameter("@Caption", photoEntry.Caption));
                command.Parameters.Add(new SqlParameter("@PhotographerId", photoEntry.Photographer.Id.IntegerId));
                command.Parameters.Add(new SqlParameter("@UploadedOn", photoEntry.UploadedOn));
                command.ExecuteNonQuery();
                photoEntry.Id.IntegerId = Convert.ToInt32(command.Parameters["@Id"].Value); //Getting value from DB without querying
            }
            return photoEntry;
        }

        /// <inheritdoc />
        public void Delete(string referenceid)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            using SqlCommand command = conncetion.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = DeleteProcedure;
            command.Parameters.Add(new SqlParameter("@Id", referenceIdMapper.GetIntegerId(referenceid, IdType.PhotoEntry)));
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Retreves a list of all the <see cref="PhotoEntry"/> in database with the given theme
        /// </summary>
        /// <param name="theme"/>
        /// <returns>Enumerable of <see cref="PhotoEntry"/></returns>
#pragma warning disable IDE0051 // Remove unused private members
        private IEnumerable<PhotoEntry> GetPhotoEntries(string theme)
#pragma warning restore IDE0051 // Remove unused private members
        {
            var photoEntries = new List<PhotoEntry>();
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                using SqlCommand command = conncetion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = GetAllProcedure; // TODO: bitmask conditions in get
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var photoEntry = new PhotoEntry(reader);
                    photoEntry.ResolveReferenceId(referenceIdMapper);
                    photoEntries.Add(photoEntry);
                }
            }
            return photoEntries;
        }
    }
}

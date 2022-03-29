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
    public class PhotoEntryProvider : IPhotoEntryProvider
    {
        private readonly string connectionString;
        private readonly IReferenceIdProvider referenceIdProvider;

        /// <summary>
        /// Initializes a new instance of PhotoEntryProvider class
        /// </summary>
        /// <param name="_dbConnection"></param>
        /// <param name="_referenceIdProvider"></param>
        public PhotoEntryProvider(
            IDbConnection _dbConnection,
            IReferenceIdProvider _referenceIdProvider
            )
        {
            if (_dbConnection is null)
            {
                throw new ArgumentNullException(nameof(_dbConnection));
            }

            connectionString = _dbConnection.ConnectionString;
            referenceIdProvider = _referenceIdProvider ?? throw new ArgumentNullException(nameof(_referenceIdProvider));
        }

        /// <inheritdoc />
        public PhotoEntry AddPhotoEntry(PhotoEntry photoEntry)
        {
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("INSERT INTO [dbo].[PhotoEntry] ( ");
                qs.Append("[Theme], ");
                qs.Append("[FileId], ");
                qs.Append("[Caption], ");
                qs.Append("[PhotographerId], ");
                qs.Append("[UploadedOn] ");
                qs.Append(") ");
                qs.Append("OUTPUT [INSERTED].[Id] ");
                qs.Append("Values (@Theme, @FileId, @Caption, @PhotographerId, @UploadedOn)");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                command.Parameters.AddWithValue("@Theme", photoEntry.Theme);
                command.Parameters.AddWithValue("@FileId", photoEntry.FileId.IntegerId);
                command.Parameters.AddWithValue("@Caption", photoEntry.Caption);
                command.Parameters.AddWithValue("@PhotographerId", photoEntry.Photographer.Id.IntegerId);
                command.Parameters.AddWithValue("@UploadedOn", photoEntry.UploadedOn);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                photoEntry.Id.IntegerId = reader.GetInt32(0);
            }

            referenceIdProvider.InsertIdMap(photoEntry.Id, IdType.PhotoEntry);
            return GetPhotoEntry(photoEntry.Id.IntegerId);
        }

        /// <inheritdoc />
        public void DeletePhotoEntry(int id)
        {
            using SqlConnection conncetion = new(connectionString);
            conncetion.Open();
            var qs = new StringBuilder();
            qs.Append("DELETE FROM [dbo].[PhotoEntry] WHERE [Id]=@Id");
            var sql = qs.ToString();
            using SqlCommand command = new(sql, conncetion);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        /// <inheritdoc />
        public IEnumerable<PhotoEntry> GetPhotoEntries()
        {
            var photoEntries = new List<PhotoEntry>();
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("SELECT ");
                qs.Append("[Id], ");
                qs.Append("[Theme], ");
                qs.Append("[FileId], ");
                qs.Append("[Caption], ");
                qs.Append("[PhotographerId], ");
                qs.Append("[UploadedOn] ");
                qs.Append("FROM [dbo].[PhotoEntry] ");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var photoEntry = new PhotoEntry
                    {
                        Id = new Id { IntegerId = reader.GetInt32(0) },
                        Theme = reader.GetString(1),
                        FileId = new Id { IntegerId = reader.GetInt32(2) },
                        Caption = reader.GetString(3),
                        // Change this to go fetch the photographer.
                        Photographer = new Photographer { Id = new Id { IntegerId = reader.GetInt32(4) } },
                        UploadedOn = reader.GetDateTime(5),
                    };
                    photoEntry.ResolveReferenceId(referenceIdProvider);
                    photoEntries.Add(photoEntry);
                }
            }
            return photoEntries;
        }

        /// <inheritdoc />
        public IEnumerable<PhotoEntry> GetPhotoEntries(string theme)
        {
            var photoEntries = new List<PhotoEntry>();
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("SELECT ");
                qs.Append("[Id], ");
                qs.Append("[Theme], ");
                qs.Append("[FileId], ");
                qs.Append("[Caption], ");
                qs.Append("[PhotographerId], ");
                qs.Append("[UploadedOn] ");
                qs.Append("FROM [dbo].[PhotoEntry] ");
                qs.Append("WHERE [Theme]=@Theme ");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                command.Parameters.AddWithValue("@Theme", theme);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var photoEntry = new PhotoEntry
                    {
                        Id = new Id { IntegerId = reader.GetInt32(0) },
                        Theme = reader.GetString(1),
                        FileId = new Id { IntegerId = reader.GetInt32(2) },
                        Caption = reader.GetString(3),
                        // Change this to go fetch the photographer.
                        Photographer = new Photographer { Id = new Id { IntegerId = reader.GetInt32(4) } },
                        UploadedOn = reader.GetDateTime(5),
                    };
                    photoEntry.ResolveReferenceId(referenceIdProvider);
                    photoEntries.Add(photoEntry);
                }
            }
            return photoEntries;
        }

        /// <inheritdoc />
        public PhotoEntry GetPhotoEntry(int id)
        {
            PhotoEntry photoEntry;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("SELECT ");
                qs.Append("[Id], ");
                qs.Append("[Theme], ");
                qs.Append("[FileId], ");
                qs.Append("[Caption], ");
                qs.Append("[PhotographerId], ");
                qs.Append("[UploadedOn] ");
                qs.Append("FROM [dbo].[PhotoEntry] ");
                qs.Append("WHERE [Id]=@Id ");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                command.Parameters.AddWithValue("@Id", id);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                photoEntry = new PhotoEntry
                {
                    Id = new Id { IntegerId = reader.GetInt32(0) },
                    Theme = reader.GetString(1),
                    FileId = new Id { IntegerId = reader.GetInt32(2) },
                    Caption = reader.GetString(3),
                    // Change this to go fetch the photographer.
                    Photographer = new Photographer { Id = new Id { IntegerId = reader.GetInt32(4) } },
                    UploadedOn = reader.GetDateTime(5),
                };
            }
            photoEntry.ResolveReferenceId(referenceIdProvider);
            return photoEntry;
        }

        /// <inheritdoc />
        public PhotoEntry UpdatePhotoEntry(PhotoEntry photoEntry)
        {
            int id;
            using (SqlConnection conncetion = new(connectionString))
            {
                conncetion.Open();
                var qs = new StringBuilder();
                qs.Append("UPDATE [dbo].[PhotoEntry] SET ");
                qs.Append("[Theme] = @Theme, ");
                qs.Append("[FileId] = @FileId, ");
                qs.Append("[Caption] = @Caption, ");
                qs.Append("[PhotographerId] = @PhotographerId, ");
                qs.Append("[UploadedOn] = @UploadedOn ");
                qs.Append("OUTPUT [INSERTED].[Id] ");
                qs.Append("WHERE [Id]=@Id ");
                var sql = qs.ToString();
                using SqlCommand command = new(sql, conncetion);
                command.Parameters.AddWithValue("@Id", photoEntry.Id.IntegerId);
                command.Parameters.AddWithValue("@Theme", photoEntry.Theme);
                command.Parameters.AddWithValue("@FileId", photoEntry.FileId.IntegerId);
                command.Parameters.AddWithValue("@Caption", photoEntry.Caption);
                command.Parameters.AddWithValue("@PhotographerId", photoEntry.Photographer.Id.IntegerId);
                command.Parameters.AddWithValue("@UploadedOn", photoEntry.UploadedOn);
                using SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
            }
            return GetPhotoEntry(id);
        }
    }
}

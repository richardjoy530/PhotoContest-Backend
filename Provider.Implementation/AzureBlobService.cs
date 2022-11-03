using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PhotoContest.Implementation
{
    public class AzureBlobService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly BlobContainerClient containerClient;

        private const string CONTAINER = "photos";

        public AzureBlobService(IConfiguration _configuration)
        {
            configuration = _configuration ?? throw new ArgumentNullException(nameof(_configuration));
            var connectionString = configuration.GetConnectionString("StorageAccount");

            containerClient = new(connectionString, CONTAINER);
            containerClient.CreateIfNotExists();
        }

        public Stream ReadFileAsync(string filename)
        {
            BlobClient blobClient = containerClient.GetBlobClient(filename);
            return blobClient.OpenReadAsync().Result;
        }

        public async Task UploadFileAsync(Stream stream, string filename)
        {
            BlobClient blobClient = containerClient.GetBlobClient(filename);
            await blobClient.UploadAsync(stream);
        }
    }
}

#region

using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

#endregion

namespace PhotoContest.Implementation;

/// <summary>
/// </summary>
public class AzureBlobService : IFileService
{
    private const string Container = "photos";
    private readonly BlobContainerClient _containerClient;

    /// <summary>
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public AzureBlobService(IConfiguration configuration)
    {
        _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
        var connectionString = configuration.GetConnectionString("StorageAccount");
        _containerClient = new BlobContainerClient(connectionString, Container);
        _containerClient.CreateIfNotExists();
    }

    /// <summary>
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Stream ReadFileAsync(string filename)
    {
        var blobClient = _containerClient.GetBlobClient(filename);
        return blobClient.OpenReadAsync().Result;
    }

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="filename"></param>
    public async Task UploadFileAsync(Stream stream, string filename)
    {
        var blobClient = _containerClient.GetBlobClient(filename);
        await blobClient.UploadAsync(stream);
    }
}
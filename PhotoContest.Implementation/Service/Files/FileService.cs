using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotoContest.Implementation.Ado.Providers;
using FileInfo = PhotoContest.Implementation.Ado.DataRecords.FileInfo;

namespace PhotoContest.Implementation.Service.Files
{
    /// <summary>
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IFileInfoProvider _fileInfoProvider;
        private readonly IFileProvider _fileProvider;

        /// <summary>
        /// </summary>
        /// <param name="fileProvider"></param>
        /// <param name="fileInfoProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public FileService(IFileProvider fileProvider, IFileInfoProvider fileInfoProvider)
        {
            _fileInfoProvider = fileInfoProvider ?? throw new ArgumentNullException(nameof(fileInfoProvider));
            _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
        }

        /// <summary>
        /// </summary>
        public async Task SaveFile(IFormFile file)
        {
            await using var stream = file.OpenReadStream();
            await _fileProvider.UploadFileAsync(stream, file.FileName);
            var data = new FileInfo
            {
                Path = file.FileName
            };
            _fileInfoProvider.Insert(data);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Stream GetFile(int id)
        {
            var fileName = _fileInfoProvider.GetById(id).Path;
            return _fileProvider.ReadFileAsync(fileName);
        }
    }
}
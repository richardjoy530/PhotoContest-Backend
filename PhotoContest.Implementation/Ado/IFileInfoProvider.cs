using System.Collections.Generic;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
/// 
/// </summary>
public interface IFileInfoProvider
{
    /// <inheritdoc />
    int Insert(FileInfo data);

    /// <inheritdoc />
    bool Delete(int id);

    /// <inheritdoc />
    FileInfo GetById(int id);

    /// <inheritdoc />
    int[] GetAllIds();

    /// <inheritdoc />
    IEnumerable<FileInfo> GetAll();

    /// <inheritdoc />
    bool Update(FileInfo data, long updateParamsLong = (long)FileInfoParams.None);
}
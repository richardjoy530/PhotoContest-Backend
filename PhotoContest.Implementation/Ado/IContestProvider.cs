using System.Collections.Generic;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
/// 
/// </summary>
public interface IContestProvider
{
    /// <inheritdoc />
    int Insert(Contest data);

    /// <inheritdoc />
    bool Delete(int id);

    /// <inheritdoc />
    Contest GetById(int id);

    /// <inheritdoc />
    int[] GetAllIds();

    /// <inheritdoc />
    IEnumerable<Contest> GetAll();

    /// <inheritdoc />
    bool Update(Contest data, long updateParamsLong = (long)ContestParams.None);
}
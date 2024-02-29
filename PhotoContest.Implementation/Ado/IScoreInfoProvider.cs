using System.Collections.Generic;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public interface IScoreInfoProvider
{
    /// <inheritdoc />
    int Insert(ScoreInfo data);

    /// <inheritdoc />
    bool Delete(int id);

    /// <inheritdoc />
    ScoreInfo GetById(int id);

    /// <inheritdoc />
    int[] GetAllIds();

    /// <inheritdoc />
    IEnumerable<ScoreInfo> GetAll();

    /// <inheritdoc />
    bool Update(ScoreInfo data, long updateParamsLong = (long)ScoreInfoParams.None);
}
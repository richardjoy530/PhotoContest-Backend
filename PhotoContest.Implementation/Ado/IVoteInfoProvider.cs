using System.Collections.Generic;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
/// 
/// </summary>
public interface IVoteInfoProvider
{
    /// <inheritdoc />
    VoteInfo GetById(int id);

    /// <inheritdoc />
    int[] GetAllIds();

    /// <inheritdoc />
    IEnumerable<VoteInfo> GetAll();

    /// <inheritdoc />
    int Insert(VoteInfo data);

    /// <inheritdoc />
    bool Update(VoteInfo data, long updateParamsLong = (long)VoteInfoParams.None);

    /// <inheritdoc />
    bool Delete(int id);
}
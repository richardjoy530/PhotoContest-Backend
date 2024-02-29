using System.Collections.Generic;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
/// 
/// </summary>
public interface ISubmissionProvider
{
    /// <inheritdoc />
    Submission GetById(int id);

    /// <inheritdoc />
    int[] GetAllIds();

    /// <inheritdoc />
    IEnumerable<Submission> GetAll();

    /// <inheritdoc />
    int Insert(Submission data);

    /// <inheritdoc />
    bool Update(Submission data, long updateParamsLong = (long)SubmissionParams.None);

    /// <inheritdoc />
    bool Delete(int id);
}
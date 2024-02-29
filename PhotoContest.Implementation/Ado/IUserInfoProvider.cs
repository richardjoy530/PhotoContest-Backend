using System.Collections.Generic;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Ado.Providers;

/// <summary>
/// 
/// </summary>
public interface IUserInfoProvider
{
    /// <inheritdoc />
    int Insert(UserInfo data);

    /// <inheritdoc />
    bool Delete(int id);

    /// <inheritdoc />
    UserInfo GetById(int id);

    /// <inheritdoc />
    int[] GetAllIds();

    /// <inheritdoc />
    IEnumerable<UserInfo> GetAll();

    /// <inheritdoc />
    bool Update(UserInfo data, long updateParamsLong = (long)UserInfoParams.None);
}
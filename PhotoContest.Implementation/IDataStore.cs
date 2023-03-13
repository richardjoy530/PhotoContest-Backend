using System.Collections.Generic;
using PhotoContest.Models;

namespace PhotoContest.Implementation;

/// <summary>
/// 
/// </summary>
public interface IDataStore
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public IDataRecord Get(int id, AssetType type);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool Delete(int id, AssetType type);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRecord"></param>
    /// <param name="type"></param>
    /// <param name="updateParams"></param>
    /// <returns></returns>
    public bool Update(IDataRecord dataRecord, AssetType type, long updateParams);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRecord"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public int Insert(IDataRecord dataRecord, AssetType type);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IEnumerable<T> GetAll<T>(AssetType type);
}
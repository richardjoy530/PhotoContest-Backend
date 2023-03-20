using System;
using System.Collections.Generic;
using PhotoContest.Models;

namespace PhotoContest.Services;

/// <summary>
/// </summary>
public interface IContestManagementService
{
    /// <summary>
    /// </summary>
    Contest CurrentContest { get; }
    
    /// <summary>
    /// </summary>
    /// <param name="theme"></param>
    /// <param name="endDate"></param>
    int Create(string theme, DateTime endDate);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    IEnumerable<Contest> GetAll();

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Delete(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="contest"></param>
    /// <returns></returns>
    bool TryGet(int id, out Contest contest);

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    bool UpdateEndDate(int id, DateTime endDate);

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="theme"></param>
    /// <returns></returns>
    bool UpdateTheme(int id, string theme);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="theme"></param>
    /// <param name="contest"></param>
    /// <returns></returns>
    public bool TryGetByTheme(string theme, out Contest contest);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="contest"></param>
    /// <returns></returns>
    public bool TryGetActiveContestOn(DateTime dateTime, out Contest contest);

    /// <summary>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    IEnumerable<Contest> GetLastContest(int count);
}
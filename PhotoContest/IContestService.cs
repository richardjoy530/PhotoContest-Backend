using System;
using PhotoContest.Models;

namespace PhotoContest;

/// <summary>
/// 
/// </summary>
public interface IContestService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="theme"></param>
    /// <param name="endDate"></param>
    int AddContest(string theme, DateTime endDate);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Contest GetCurrentContest();
}
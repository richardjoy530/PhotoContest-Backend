using System;
using System.Collections.Generic;
using PhotoContest.Models;

namespace PhotoContest;

/// <summary>
/// </summary>
public interface IContestManagementService
{
    /// <summary>
    /// </summary>
    /// <param name="theme"></param>
    /// <param name="endDate"></param>
    int Create(string theme, DateTime endDate);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    Contest GetCurrentContest();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    IEnumerable<Contest> GetAllContests();
}
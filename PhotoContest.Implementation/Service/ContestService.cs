using System;
using System.Collections.Generic;
using System.Linq;
using PhotoContest.Implementation.Ado.DataRecords;

namespace PhotoContest.Implementation.Service;

/// <summary>
/// </summary>
public class ContestService : IContestService
{
    private readonly IProvider<Contest> _contestProvider;

    /// <summary>
    /// </summary>
    /// <param name="contestProvider"></param>
    public ContestService(IProvider<Contest> contestProvider)
    {
        _contestProvider = contestProvider ?? throw new ArgumentNullException(nameof(contestProvider));
    }

    /// <summary>
    /// </summary>
    public int AddContest(string theme, DateTime endDate)
    {
        var contest = new Contest();
        contest.Theme = theme;
        contest.EndDate = endDate;
        return _contestProvider.Insert(contest);
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Models.Contest GetCurrentContest()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Models.Contest> GetAllContests()
    {
        return _contestProvider.GetAll().Select(ToModel);
    }

    private Models.Contest ToModel(Contest arg)
    {
        return new Models.Contest
        {
            EndDate = arg.EndDate,
            Id = arg.Id,
            Theme = arg.Theme
        };
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PhotoContest.Implementation.Ado;
using PhotoContest.Implementation.Ado.DataRecords;
using PhotoContest.Services;
using Contest = PhotoContest.Models.Contest;

namespace PhotoContest.Implementation.Service
{
    /// <summary>
    /// </summary>
    public class ContestManagementService : IContestManagementService
    {
        private readonly IProvider<Ado.DataRecords.Contest> _dataStore;

        private static Contest _currentContest;
    
        /// <summary>
        /// </summary>
        public Contest CurrentContest
        {
            get
            {
                if (_currentContest == null || _currentContest.EndDate < DateTime.Now)
                {
                    var maxDate = _dataStore.GetAll().Max(c => c.EndDate);
                    TryGetActiveContestOn(maxDate, out _currentContest);
                }
                return _currentContest;
            }
        }
    
        /// <summary>
        /// </summary>
        public ContestManagementService(IProvider<Ado.DataRecords.Contest> dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        /// <inheritdoc />
        public int Create(string theme, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(theme)) throw new ValidationException("Theme is invalid");
            if (endDate < DateTime.Now) throw new ValidationException("End date must be a date in the future");
            if (endDate > DateTime.Today.AddMonths(1))  throw new ValidationException("End date must be within a month from now");
        
            var contestRecord = new Ado.DataRecords.Contest(theme: theme, endDate: endDate);
            return _dataStore.Insert(contestRecord);
        }

        /// <inheritdoc />
        public IEnumerable<Contest> GetAll()
        {
            return _dataStore.GetAll().Select(Converters.ToModel);
        }

        /// <inheritdoc />
        public bool Delete(int id)
        {
            if (id < 1) throw new ValidationException("Integer id must not be less than 1");

            return _dataStore.Delete(id);
        }

        /// <inheritdoc />
        public bool TryGet(int id, out Contest contest)
        {
            if (id < 1) throw new ValidationException("Integer id must not be less than 1");

            Ado.DataRecords.Contest dataRecord;
            if ((dataRecord = _dataStore.GetById(id)) == null)
            {
                contest = default;
                return false;
            }
            contest = Converters.ToModel(dataRecord);
            return true;
        }

        /// <inheritdoc />
        public bool UpdateEndDate(int id, DateTime endDate)
        {
            if (id < 1) throw new ValidationException("Integer id must not be less than 1");
            if (endDate < DateTime.Now) throw new ValidationException("End date must be a date in the future");
            if (endDate > DateTime.Today.AddMonths(1))  throw new ValidationException("End date must be within a month from now");
        
            var dataRecord = _dataStore.GetById(id);
            return _dataStore.Update(dataRecord, (long)ContestParams.EndDate);
        }

        /// <inheritdoc />
        public bool UpdateTheme(int id, string theme)
        {
            if (id < 1) throw new ValidationException("Integer id must not be less than 1");
            if (string.IsNullOrWhiteSpace(theme)) throw new ValidationException("Theme is invalid");

            var dataRecord = _dataStore.GetById(id);
            return _dataStore.Update(dataRecord, (long)ContestParams.Theme);
        }

        /// <inheritdoc />
        public bool TryGetByTheme(string theme, out Contest contest)
        {
            var dataRecord = _dataStore.GetAll().FirstOrDefault(c => c.Theme == theme);
            if (dataRecord == null)
            {
                contest = default;
                return false;
            }

            contest = Converters.ToModel(dataRecord);
            return true;
        }

        //todo: optimise
        /// <inheritdoc />
        public bool TryGetActiveContestOn(DateTime dateTime, out Contest contest)
        {
            var dataRecords = _dataStore.GetAll().ToList();
            dataRecords.Sort((c, v) => c.EndDate.CompareTo(v.EndDate));
            contest = Converters.ToModel(dataRecords.FirstOrDefault(c => c.EndDate <= dateTime));
            return contest != default;
        }

        /// <inheritdoc />
        public IEnumerable<Contest> GetLastContest(int count)
        {
            var dataRecords = _dataStore.GetAll().ToList();
            dataRecords.Sort((c, v) => c.EndDate.CompareTo(v));
            return dataRecords.TakeLast(count).Select(Converters.ToModel);
        }
    }
}
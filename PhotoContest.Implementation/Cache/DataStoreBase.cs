using System;
using System.Collections.Generic;
using PhotoContest.Models;
using Contest = PhotoContest.Implementation.Ado.DataRecords.Contest;
using FileInfo = PhotoContest.Implementation.Ado.DataRecords.FileInfo;
using ScoreInfo = PhotoContest.Implementation.Ado.DataRecords.ScoreInfo;
using Submission = PhotoContest.Implementation.Ado.DataRecords.Submission;
using UserInfo = PhotoContest.Implementation.Ado.DataRecords.UserInfo;
using VoteInfo = PhotoContest.Implementation.Ado.DataRecords.VoteInfo;

namespace PhotoContest.Implementation.Cache;

internal class DataStoreBase
{
    private IDictionary<(AssetType Type, int Id), IDataRecord> _inMemoryDataRecordMap;
    private IDictionary<AssetType, IEnumerable<int>> _identityMap;
    private readonly IProvider<Contest> _contestProvider;
    private readonly IProvider<UserInfo> _userInfoProvider;
    private readonly IProvider<FileInfo> _fileInfoProvider;
    private readonly IProvider<VoteInfo> _voteInfoProvider;
    private readonly IProvider<Submission> _submissionProvider;
    private readonly IProvider<ScoreInfo> _scoreInfoProvider;

    private IDictionary<(AssetType Type, int Id), IDataRecord> InMemoryDataRecordMap
    {
        get
        {
            lock (Locker)
            {
                if (_inMemoryDataRecordMap != null)
                    return _inMemoryDataRecordMap;
                else
                    return _inMemoryDataRecordMap = new Dictionary<(AssetType Type, int Id), IDataRecord>();
            }
        }
    }
    
    private IDictionary<AssetType, IEnumerable<int>> IdentityMap
    {
        get
        {
            lock (LockerId)
            {
                if (_identityMap != null)
                    return _identityMap;
                else
                    return _identityMap = new Dictionary<AssetType, IEnumerable<int>>();
            }
        }
    }
    
    
    public DataStoreBase(IProvider<Contest> contestProvider,
        IProvider<UserInfo> userInfoProvider,
        IProvider<FileInfo> fileInfoProvider,
        IProvider<VoteInfo> voteInfoProvider,
        IProvider<Submission> submissionProvider,
        IProvider<ScoreInfo> scoreInfoProvider)
    {
        _contestProvider = contestProvider ?? throw new ArgumentNullException(nameof(contestProvider));
        _userInfoProvider = userInfoProvider ?? throw new ArgumentNullException(nameof(userInfoProvider));
        _fileInfoProvider = fileInfoProvider ?? throw new ArgumentNullException(nameof(fileInfoProvider));
        _voteInfoProvider = voteInfoProvider ?? throw new ArgumentNullException(nameof(voteInfoProvider));
        _submissionProvider = submissionProvider ?? throw new ArgumentNullException(nameof(submissionProvider));
        _scoreInfoProvider = scoreInfoProvider ?? throw new ArgumentNullException(nameof(scoreInfoProvider));
    }
    
    public IDataRecord GetRecord(int id, AssetType type, bool replaceCache = false)
    {
        switch (type)
        {
            case AssetType.Contest: return GetDataRecord(id, type, i => _contestProvider.GetById(i), replaceCache);
            case AssetType.FileInfo: return GetDataRecord(id, type, i => _fileInfoProvider.GetById(i), replaceCache);
            case AssetType.ScoreInfo: return GetDataRecord(id, type, i => _scoreInfoProvider.GetById(i), replaceCache);
            case AssetType.Submission: return GetDataRecord(id, type, i => _submissionProvider.GetById(i), replaceCache);
            case AssetType.UserInfo: return GetDataRecord(id, type, i => _userInfoProvider.GetById(i), replaceCache);
            case AssetType.VoteInfo: return GetDataRecord(id, type, i => _voteInfoProvider.GetById(i), replaceCache);
        }

        throw new InvalidOperationException("Unable to get the type of the asset");
    }

    public bool DeleteDataRecord(int id, AssetType type)
    {
        var pass = false;
        switch (type)
        {
            case AssetType.Contest: pass = _contestProvider.Delete(id); break;
            case AssetType.FileInfo: pass = _fileInfoProvider.Delete(id); break;
            case AssetType.ScoreInfo: pass = _scoreInfoProvider.Delete(id); break;
            case AssetType.Submission: pass = _submissionProvider.Delete(id); break;
            case AssetType.UserInfo: pass = _userInfoProvider.Delete(id); break;
            case AssetType.VoteInfo: pass = _voteInfoProvider.Delete(id); break;
        }

        if (!pass) return false;
        InMemoryDataRecordMap.Remove((type, id));
        return true;
    }

    public bool UpdateDataRecord(IDataRecord dataRecord, AssetType type, long updateParams)
    {
        var pass = false;
        switch (type)
        {
            case AssetType.Contest: pass = _contestProvider.Update((Contest)dataRecord, updateParams); break;
            case AssetType.FileInfo: pass = _fileInfoProvider.Update((FileInfo)dataRecord, updateParams); break;
            case AssetType.ScoreInfo: pass = _scoreInfoProvider.Update((ScoreInfo)dataRecord, updateParams); break;
            case AssetType.Submission: pass = _submissionProvider.Update((Submission)dataRecord, updateParams); break;
            case AssetType.UserInfo: pass = _userInfoProvider.Update((UserInfo)dataRecord, updateParams); break;
            case AssetType.VoteInfo: pass = _voteInfoProvider.Update((VoteInfo)dataRecord, updateParams); break;
        }
        
        if (!pass) return false;
        GetRecord(dataRecord.Id, type, true);
        return true;
    }

    public int InsertDataRecord(IDataRecord dataRecord, AssetType type)
    {
        switch (type)
        {
            case AssetType.Contest: _contestProvider.Insert((Contest)dataRecord); break;
            case AssetType.FileInfo: _fileInfoProvider.Insert((FileInfo)dataRecord); break;
            case AssetType.ScoreInfo: _scoreInfoProvider.Insert((ScoreInfo)dataRecord); break;
            case AssetType.Submission: _submissionProvider.Insert((Submission)dataRecord); break;
            case AssetType.UserInfo: _userInfoProvider.Insert((UserInfo)dataRecord); break;
            case AssetType.VoteInfo: _voteInfoProvider.Insert((VoteInfo)dataRecord); break;
        }
        
        GetRecord(dataRecord.Id, type, true);
        return dataRecord.Id;
    }

    public IEnumerable<T> GetAll<T>(AssetType type)
    {
        //todo: IsDeleted SPs, GetAllIds, Checks for all the IsDeleted
        int[] ids;
        switch (type)
        {
            case AssetType.Contest: ids = _contestProvider.GetAllIds(); break;
            case AssetType.FileInfo: ids = _fileInfoProvider.GetAllIds(); break;
            case AssetType.ScoreInfo: ids = _scoreInfoProvider.GetAllIds(); break;
            case AssetType.Submission: ids = _submissionProvider.GetAllIds(); break;
            case AssetType.UserInfo: ids = _userInfoProvider.GetAllIds(); break;
            case AssetType.VoteInfo: ids = _voteInfoProvider.GetAllIds(); break;
            default: ids = new int[] { }; break;
        }

        return null;
    } 

    private IDataRecord GetDataRecord(int id, AssetType type, Func<int, IDataRecord> handler, bool replaceCache)
    {
        var inCache = false;
        if (!(inCache = InMemoryDataRecordMap.TryGetValue((type, id), out var dataRecord)) || replaceCache)
        {
            // i.e not in cache or need to be force replaced
            dataRecord = handler(id);
            
            // there could be scenario where replace cache is called even without the dataRecord present in the cache
            if (replaceCache && inCache) InMemoryDataRecordMap[(type, id)] = dataRecord;
            else InMemoryDataRecordMap.Add(new KeyValuePair<(AssetType Type, int Id), IDataRecord>((type, id), dataRecord));
        }

        return dataRecord;
    }
    
    
    
    
    
    
#pragma warning disable CS0649
    private static readonly object Locker;
    private static readonly object LockerId;
#pragma warning restore CS0649
}

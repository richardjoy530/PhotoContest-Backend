using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhotoContest.Models;
using Contest = PhotoContest.Implementation.Ado.DataRecords.Contest;
using FileInfo = PhotoContest.Implementation.Ado.DataRecords.FileInfo;
using ScoreInfo = PhotoContest.Implementation.Ado.DataRecords.ScoreInfo;
using Submission = PhotoContest.Implementation.Ado.DataRecords.Submission;
using UserInfo = PhotoContest.Implementation.Ado.DataRecords.UserInfo;
using VoteInfo = PhotoContest.Implementation.Ado.DataRecords.VoteInfo;

namespace PhotoContest.Implementation.Cache;

/// <summary>
/// </summary>
public class CachedDataStore : IDataStore
{
    private IDictionary<(AssetType Type, int Id), IDataRecord> _inMemoryDataRecordMap;
    private IDictionary<AssetType, HashSet<int>> _identityMap;
    
    private bool _cachedAllContest;
    private bool _cachedAllUserInfo;
    private bool _cachedAllFileInfo;
    private bool _cachedAllVoteInfo;
    private bool _cachedAllSubmission;
    private bool _cachedAllScoreInfo;
    
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
    
    private IDictionary<AssetType, HashSet<int>> IdentityMap
    {
        get
        {
            lock (LockerId)
            {
                if (_identityMap != null)
                    return _identityMap;
                else
                    return _identityMap = new Dictionary<AssetType, HashSet<int>>();
            }
        }
    }
    
    /// <summary>
    /// </summary>
    /// <param name="contestProvider"></param>
    /// <param name="userInfoProvider"></param>
    /// <param name="fileInfoProvider"></param>
    /// <param name="voteInfoProvider"></param>
    /// <param name="submissionProvider"></param>
    /// <param name="scoreInfoProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CachedDataStore(IProvider<Contest> contestProvider,
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

    /// <inheritdoc/>
    public IDataRecord Get(int id, AssetType type) => GetCached(id, type);
    
    /// <inheritdoc/>
    public bool Delete(int id, AssetType type)
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
    
    /// <inheritdoc/>
    public bool Update(IDataRecord dataRecord, AssetType type, long updateParams)
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
        GetCached(dataRecord.Id, type, true);
        return true;
    }
    
    /// <inheritdoc/>
    public int Insert(IDataRecord dataRecord, AssetType type)
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
        
        GetCached(dataRecord.Id, type, true);
        return dataRecord.Id;
    }
    
    //todo : optimise bulk scenarios
    /// <inheritdoc/>
    public IEnumerable<T> GetAll<T>(AssetType type)
    {
        switch (type)
        {
            case AssetType.Contest: GetAllIdsHandler(type, () => _contestProvider.GetAllIds(), ref _cachedAllContest);  break;
            case AssetType.FileInfo: GetAllIdsHandler(type, () => _fileInfoProvider.GetAllIds(), ref _cachedAllFileInfo);  break;
            case AssetType.ScoreInfo: GetAllIdsHandler(type, () => _scoreInfoProvider.GetAllIds(), ref _cachedAllScoreInfo);  break;
            case AssetType.Submission: GetAllIdsHandler(type, () => _submissionProvider.GetAllIds(), ref _cachedAllSubmission);  break;
            case AssetType.UserInfo: GetAllIdsHandler(type, () => _userInfoProvider.GetAllIds(), ref _cachedAllUserInfo);  break;
            case AssetType.VoteInfo: GetAllIdsHandler(type, () => _voteInfoProvider.GetAllIds(), ref _cachedAllVoteInfo);  break;
        }

        var dataRecords = new Collection<T>();
        for (var id = 0; id < IdentityMap[type].Count; id++)
        {
            dataRecords.Add((T)Get(id, type));
        }

        return dataRecords;
    }

    private IDataRecord GetCached(int id, AssetType type, bool replaceCache = false)
    {
        switch (type)
        {
            case AssetType.Contest: return GetDataHandler(id, type, i => _contestProvider.GetById(i), replaceCache);
            case AssetType.FileInfo: return GetDataHandler(id, type, i => _fileInfoProvider.GetById(i), replaceCache);
            case AssetType.ScoreInfo: return GetDataHandler(id, type, i => _scoreInfoProvider.GetById(i), replaceCache);
            case AssetType.Submission: return GetDataHandler(id, type, i => _submissionProvider.GetById(i), replaceCache);
            case AssetType.UserInfo: return GetDataHandler(id, type, i => _userInfoProvider.GetById(i), replaceCache);
            case AssetType.VoteInfo: return GetDataHandler(id, type, i => _voteInfoProvider.GetById(i), replaceCache);
        }

        throw new InvalidOperationException("Unable to get the type of the asset");
    }

    private void GetAllIdsHandler(AssetType type, Func<int[]> handler, ref bool isCached)
    {
        if (isCached) return;
        IdentityMap[type] = handler().ToHashSet();
        isCached = true;
    }

    private IDataRecord GetDataHandler(int id, AssetType type, Func<int, IDataRecord> handler, bool replaceCache)
    {
        bool inCache;
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
    private static readonly object Locker = 123;
    private static readonly object LockerId = 456;
#pragma warning restore CS0649
}
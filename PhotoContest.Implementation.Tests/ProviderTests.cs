using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Implementation.Ado;
using FileInfo = PhotoContest.Implementation.Ado.FileInfo;

namespace PhotoContest.Implementation.Tests;

public class ProviderTests
{
    private static readonly string ConnectionString = "Server=localhost;Database=FridayDatabase;Trusted_Connection=yes;";
    private IProvider<Contest> ContestProvider { get; set; }
    private IProvider<FileInfo> FileInfoProvider { get; set; }
    private IProvider<Submission> SubmissionProvider { get; set; }
    private IProvider<UserInfo> UserInfoProvider { get; set; }
    private IProvider<VoteInfo> VoteInfoProvider { get; set; }
    private IProvider<ScoreInfo> ScoreInfoProvider { get; set; }
    private static IList<KeyValuePair<RecordType, int>> CleanupIdentityMap { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        serviceCollection.AddSingleton<IDbConnection>(_ => new SqlConnection(ConnectionString));
        PhotoContest.Implementation.DependencyInjection.ConfigureServices(serviceCollection, true);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        ContestProvider = serviceProvider.GetService<IProvider<Contest>>();
        FileInfoProvider = serviceProvider.GetService<IProvider<FileInfo>>();
        SubmissionProvider = serviceProvider.GetService<IProvider<Submission>>();
        UserInfoProvider = serviceProvider.GetService<IProvider<UserInfo>>();
        VoteInfoProvider = serviceProvider.GetService<IProvider<VoteInfo>>();
        ScoreInfoProvider = serviceProvider.GetService<IProvider<ScoreInfo>>();

        CleanupIdentityMap = new List<KeyValuePair<RecordType, int>>();
    }

    [TestCase("Sceen Chako", "sceen.chako@gmail.com")]
    [TestCase("Chako Fernandus", "chucki.chako@gmail.com")]
    public void UserProviderTests(string name, string email)
    {
        var userInfo = new UserInfo
        {
            Name = name,
            Email = email,
            RegistrationDate = DateTime.Today,
            RefId = Guid.NewGuid().ToString()
        };
        
        //Insert
        UserInfoProvider.Insert(userInfo);
        
        //Get by id
        UserInfo EnsureInfoById(UserInfo expected)
        {
            var actual = UserInfoProvider.GetById(expected.Id);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.RegistrationDate, actual.RegistrationDate);
            Assert.AreEqual(expected.RefId, actual.RefId);
            return actual;
        }
        EnsureInfoById(userInfo);
        
        //Update
        userInfo.Email = $"updated_{email}";
        userInfo.Name = $"updated_{name}";
        UserInfoProvider.Update(userInfo, userInfo.Id);
        EnsureInfoById(userInfo);
        
        CleanupIdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.UserInfo, userInfo.Id));
    }

    [TestCase("Heroes", "2023-05-27")]
    [TestCase("Grant", "2023-06-27")]
    public void ContestProviderTests(string theme, string dateTimeString)
    {
        var contest = new Contest
        {
            EndDate = DateTime.Parse(dateTimeString),
            Theme = theme,
        };
        
        //Insert
        ContestProvider.Insert(contest);
        
        //Get by id
        Contest EnsureInfoById(Contest expected)
        {
            var actual = ContestProvider.GetById(expected.Id);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Theme, actual.Theme);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            return actual;
        }
        EnsureInfoById(contest);
        
        //Update
        contest.Theme = $"updated_{theme}";
        contest.EndDate = DateTime.Parse(dateTimeString).AddDays(5);
        ContestProvider.Update(contest, contest.Id);
        EnsureInfoById(contest);
        
        CleanupIdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.Contest, contest.Id));
    }

    [TestCase("https://learn.microsoft.com/en-us/s-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16")]
    [TestCase("https://server-management-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16")]
    public void FileInfoProviderTests(string path)
    {
        var fileInfo = new FileInfo
        {
            Path = path,
        };
        
        //Insert
        FileInfoProvider.Insert(fileInfo);
        
        //Get by id
        FileInfo EnsureInfoById(FileInfo expected)
        {
            var actual = FileInfoProvider.GetById(expected.Id);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Path, actual.Path);
            return actual;
        }
        EnsureInfoById(fileInfo);
        
        //Update
        fileInfo.Path = $"updated_{path}";
        FileInfoProvider.Update(fileInfo, fileInfo.Id);
        EnsureInfoById(fileInfo);
        
        CleanupIdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.FileInfo, fileInfo.Id));
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        foreach (var kvp in CleanupIdentityMap)
        {
            switch (kvp.Key)
            {
                case RecordType.UserInfo: Assert.IsTrue(UserInfoProvider.Delete(kvp.Value));
                    break;
                case RecordType.ScoreInfo: Assert.IsTrue(ScoreInfoProvider.Delete(kvp.Value));
                    break;
                case RecordType.Contest: Assert.IsTrue(ContestProvider.Delete(kvp.Value));
                    break;
                case RecordType.VoteInfo: Assert.IsTrue(VoteInfoProvider.Delete(kvp.Value));
                    break;
                case RecordType.Submission: Assert.IsTrue(SubmissionProvider.Delete(kvp.Value));
                    break;
                case RecordType.FileInfo: Assert.IsTrue(FileInfoProvider.Delete(kvp.Value));
                    break;
            }
        }
    }
}

internal enum RecordType
{
    Contest,
    ScoreInfo,
    UserInfo,
    VoteInfo,
    FileInfo,
    Submission
}
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
    private static IList<KeyValuePair<RecordType, int>> IdentityMap { get; set; }

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

        IdentityMap = new List<KeyValuePair<RecordType, int>>();
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
        
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.UserInfo, userInfo.Id));
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
        
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.Contest, contest.Id));
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
        
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.FileInfo, fileInfo.Id));
    }

    private void InsertUserInfo(string name, string email)
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
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.UserInfo, userInfo.Id));
    }

    private void InsertContest(string theme, string dateTimeString)
    {
        var contest = new Contest
        {
            EndDate = DateTime.Parse(dateTimeString),
            Theme = theme,
        };
        
        //Insert
        ContestProvider.Insert(contest);
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.Contest, contest.Id));
    }

    private void InsertFileInfo(string path)
    {
        var fileInfo = new FileInfo
        {
            Path = path,
        };
        
        //Insert
        FileInfoProvider.Insert(fileInfo);
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.FileInfo, fileInfo.Id));
    }

    [Test]
    public void SubmissionProviderTests()
    {
        InsertUserInfo("Jimbru", "jimbru.rocks@hotmail.com");
        InsertContest("Circus Time 1 2 3", "2019-01-01");
        InsertFileInfo("https://github.com/actions/setup-dotnet");
        var submissionInfo = new Submission
        {
            UserId = IdentityMap.Where(kv => kv.Key == RecordType.UserInfo).Select(kv => kv.Value).First(),
            Caption = "The best photo ever",
            UploadedOn = DateTime.Parse("2019-02-01"),
            FileInfoId = IdentityMap.Where(kv => kv.Key == RecordType.FileInfo).Select(kv => kv.Value).First(),
            ContestId = IdentityMap.Where(kv => kv.Key == RecordType.Contest).Select(kv => kv.Value).First(),
            RefId = Guid.NewGuid().ToString()
        };
        
        //Insert
        SubmissionProvider.Insert(submissionInfo);
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.Submission, submissionInfo.Id));

        //Get by id
        Submission EnsureInfoById(Submission expected)
        {
            var actual = SubmissionProvider.GetById(expected.Id);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Caption, actual.Caption);
            Assert.AreEqual(expected.UploadedOn, actual.UploadedOn);
            Assert.AreEqual(expected.FileInfoId, actual.FileInfoId);
            Assert.AreEqual(expected.ContestId, actual.ContestId);
            Assert.AreEqual(expected.RefId, actual.RefId);
            return actual;
        }
        EnsureInfoById(submissionInfo);
        
        //Update
        submissionInfo.Caption = $"updated_{submissionInfo.Caption}";
        submissionInfo.UploadedOn = submissionInfo.UploadedOn.AddDays(5);

        SubmissionProvider.Update(submissionInfo, submissionInfo.Id);
        EnsureInfoById(submissionInfo);
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        foreach (var id in IdentityMap.Where(kv => kv.Key == RecordType.VoteInfo).Select(kv => kv.Value))
            Assert.IsTrue(VoteInfoProvider.Delete(id));
        
        foreach (var id in IdentityMap.Where(kv => kv.Key == RecordType.ScoreInfo).Select(kv => kv.Value))
            Assert.IsTrue(ScoreInfoProvider.Delete(id));
        
        foreach (var id in IdentityMap.Where(kv => kv.Key == RecordType.Submission).Select(kv => kv.Value))
            Assert.IsTrue(SubmissionProvider.Delete(id));
        
        foreach (var id in IdentityMap.Where(kv => kv.Key == RecordType.FileInfo).Select(kv => kv.Value))
            Assert.IsTrue(FileInfoProvider.Delete(id));
        
        foreach (var id in IdentityMap.Where(kv => kv.Key == RecordType.UserInfo).Select(kv => kv.Value))
            Assert.IsTrue(UserInfoProvider.Delete(id));
        
        foreach (var id in IdentityMap.Where(kv => kv.Key == RecordType.Contest).Select(kv => kv.Value))
            Assert.IsTrue(ContestProvider.Delete(id));
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
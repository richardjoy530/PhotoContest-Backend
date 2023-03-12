using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using PhotoContest.Implementation.Ado.DataRecords;
using FileInfo = PhotoContest.Implementation.Ado.DataRecords.FileInfo;

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
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IDbConnection>(_ => new SqlConnection(ConnectionString));
        serviceCollection.ConfigureServices(true);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        ContestProvider = serviceProvider.GetService<IProvider<Contest>>() ?? throw new InvalidOperationException();
        FileInfoProvider = serviceProvider.GetService<IProvider<FileInfo>>() ?? throw new InvalidOperationException();
        SubmissionProvider = serviceProvider.GetService<IProvider<Submission>>() ?? throw new InvalidOperationException();
        UserInfoProvider = serviceProvider.GetService<IProvider<UserInfo>>() ?? throw new InvalidOperationException();
        VoteInfoProvider = serviceProvider.GetService<IProvider<VoteInfo>>() ?? throw new InvalidOperationException();
        ScoreInfoProvider = serviceProvider.GetService<IProvider<ScoreInfo>>() ?? throw new InvalidOperationException();

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
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Email, Is.EqualTo(expected.Email));
            Assert.That(actual.RegistrationDate, Is.EqualTo(expected.RegistrationDate));
            Assert.That(actual.RefId, Is.EqualTo(expected.RefId));
            return actual;
        }

        EnsureInfoById(userInfo);

        //Update
        userInfo.Email = $"updated_{email}";
        userInfo.Name = $"updated_{name}";
        var updateParams = UserInfoParams.Email | UserInfoParams.Name;
        UserInfoProvider.Update(userInfo, (long)updateParams);
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
            Theme = theme
        };

        //Insert
        ContestProvider.Insert(contest);

        //Get by id
        Contest EnsureInfoById(Contest expected)
        {
            var actual = ContestProvider.GetById(expected.Id);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Theme, Is.EqualTo(expected.Theme));
            Assert.That(actual.EndDate, Is.EqualTo(expected.EndDate));
            return actual;
        }

        EnsureInfoById(contest);

        //Update
        contest.Theme = $"updated_{theme}";
        contest.EndDate = DateTime.Parse(dateTimeString).AddDays(5);
        var updateParams = ContestParams.Theme | ContestParams.EndDate;
        ContestProvider.Update(contest, (long)updateParams);
        EnsureInfoById(contest);

        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.Contest, contest.Id));
    }

    [TestCase("https://learn.microsoft.com/en-us/s-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16")]
    [TestCase("https://server-management-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16")]
    public void FileInfoProviderTests(string path)
    {
        var fileInfo = new FileInfo
        {
            Path = path
        };

        //Insert
        FileInfoProvider.Insert(fileInfo);

        //Get by id
        FileInfo EnsureInfoById(FileInfo expected)
        {
            var actual = FileInfoProvider.GetById(expected.Id);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Path, Is.EqualTo(expected.Path));
            return actual;
        }

        EnsureInfoById(fileInfo);

        //Update
        fileInfo.Path = $"updated_{path}";
        var updateParams = FileInfoParams.Path;
        FileInfoProvider.Update(fileInfo, (long)updateParams);
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
            Theme = theme
        };

        //Insert
        ContestProvider.Insert(contest);
        IdentityMap.Add(new KeyValuePair<RecordType, int>(RecordType.Contest, contest.Id));
    }

    private void InsertFileInfo(string path)
    {
        var fileInfo = new FileInfo
        {
            Path = path
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
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Caption, Is.EqualTo(expected.Caption));
            Assert.That(actual.UploadedOn, Is.EqualTo(expected.UploadedOn));
            Assert.That(actual.FileInfoId, Is.EqualTo(expected.FileInfoId));
            Assert.That(actual.ContestId, Is.EqualTo(expected.ContestId));
            Assert.That(actual.RefId, Is.EqualTo(expected.RefId));
            return actual;
        }

        EnsureInfoById(submissionInfo);

        //Update
        submissionInfo.Caption = $"updated_{submissionInfo.Caption}";
        submissionInfo.UploadedOn = submissionInfo.UploadedOn.AddDays(5);
        var updateParams = SubmissionParams.Caption | SubmissionParams.UploadedOn;
        SubmissionProvider.Update(submissionInfo, (long)updateParams);
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